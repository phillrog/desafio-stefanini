import { Component, OnInit, signal } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { ProdutoResponse } from '../../shared/models/pedido.model';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-pedido-form',
  templateUrl: './pedido-form.component.html',
  styleUrls: ['./pedido-form.component.css'],
  standalone: false
})
export class PedidoFormComponent implements OnInit {
  pedidoForm!: FormGroup;
  produtos = signal<ProdutoResponse[]>([]);
  isEditando = signal<boolean>(false);
  loading = signal<boolean>(false);
  pedidoId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.iniciarForm();
    this.carregarProdutos();

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.pedidoId = id;
        this.isEditando.set(true);
        this.loadPedido(id);
      }
    });
  }

  iniciarForm(): void {
    this.pedidoForm = this.fb.group({
      nomeCliente: ['', Validators.required],
      emailCliente: ['', [Validators.required, Validators.email]],
      pago: [false],
      itens: this.fb.array([], Validators.required)
    });

    if (!this.isEditando()) {
      this.adicionarItem();
    }
  }

  get itens() {
    return this.pedidoForm.get('itens') as FormArray;
  }

  criarItem(): FormGroup {
    return this.fb.group({
      idProduto: [null, Validators.required],
      quantidade: [1, [Validators.required, Validators.min(1)]]
    });
  }

  adicionarItem(): void {
    this.itens.push(this.criarItem());
  }

  removerItem(index: number): void {
    this.itens.removeAt(index);
  }

  carregarProdutos(): void {
    this.apiService.getProdutos().subscribe({
      next: (data) => {
        this.produtos.set(Array.isArray(data) ? data : []);
      },
      error: (err) => {
        console.error('Erro ao carregar produtos', err);
        this.produtos.set([]);
      }
    });
  }

  loadPedido(id: string): void {
    this.loading.set(true);
    this.apiService.getPedidoById(id).subscribe({
      next: (pedido) => {
        this.pedidoForm.patchValue({
          nomeCliente: pedido.nomeCliente,
          emailCliente: pedido.emailCliente,
          pago: pedido.pago
        });

        if (this.isEditando()) {
          this.pedidoForm.get('nomeCliente')?.disable();
          this.pedidoForm.get('emailCliente')?.disable();
        }
        
        this.itens.clear();
        pedido.itensPedido.forEach((item: any) => {
          const group = this.criarItem();
          group.patchValue({ idProduto: item.idProduto, quantidade: item.quantidade });
          this.itens.push(group);
        });
        this.loading.set(false);
      },
      error: () => this.voltar()
    });
  }

  onSubmit(): void {
    if (this.pedidoForm.invalid) {
      this.pedidoForm.markAllAsTouched();
      return;
    }

    this.confirmationService.confirm({
      message: this.isEditando() 
        ? 'Deseja realmente salvar as alterações deste pedido?' 
        : 'Confirma a criação deste novo pedido?',
      header: 'Confirmar Ação',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Confirmar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-primary',
      rejectButtonStyleClass: 'p-button-text',
      accept: () => {
        this.salvar();
      }
    });
  }

  voltar(): void {
    this.router.navigate(['/pedidos']);
  }

  isFieldInvalid(name: string): boolean {
    const control = this.pedidoForm.get(name);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }

  isItemInvalid(index: number, name: string): boolean {
    const control = this.itens.at(index).get(name);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }

  private salvar(): void {
    const formValue = this.pedidoForm.getRawValue();
    const request = {
      nomeCliente: formValue.nomeCliente,
      emailCliente: formValue.emailCliente,
      pago: formValue.pago,
      itens: formValue.itens.map((i: any) => ({
        idProduto: i.idProduto,
        quantidade: i.quantidade
      }))
    };

    if (this.isEditando() && this.pedidoId) {
      this.apiService.updatePedido(this.pedidoId, { id: this.pedidoId, ...request }).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Pedido atualizado!' });
          this.voltar();
        }
      });
    } else {
      this.apiService.createPedido(request).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Pedido criado!' });
          this.voltar();
        }
      });
    }
  }
}
