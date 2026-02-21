import { Component, computed, OnInit, signal } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { PedidoResponse } from '../../shared/models/pedido.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { debounceTime } from 'rxjs/operators';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-pedidos-list',
  templateUrl: './pedidos-list.component.html',
  styleUrls: ['./pedidos-list.component.css'],
  standalone: false
})
export class PedidosListComponent implements OnInit {
  pedidos = signal<any[]>([]);
  loading = signal<boolean>(true);
  skeletons = Array(5).fill(0);
  searchControl = new FormControl('');
  
  filterText = toSignal(
    this.searchControl.valueChanges.pipe(debounceTime(0)), 
    { initialValue: '' }
  );

  pedidosFiltrados = computed(() => {
    const termo = this.filterText()?.toLowerCase() || '';
    const lista = this.pedidos();
    if (!termo) return lista;

    return lista.filter(p => {
      const nome = p.nomeCliente?.toLowerCase() || '';
      const email = p.emailCliente?.toLowerCase() || '';
      const status = p.pago ? 'concluído pago' : 'pendente';
      const valor = p.valorTotal?.toString() || '';
      return nome.includes(termo) || email.includes(termo) || status.includes(termo) || valor.includes(termo);
    });
  });

  totalGeral = computed(() => {
    return this.pedidosFiltrados().reduce((acc, p) => acc + (Number(p.valorTotal) || 0), 0);
  });

  constructor(
    private apiService: ApiService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.carregarPedidos();
  }

  carregarPedidos(): void {
    this.loading.set(true);
    this.apiService.getPedidos().subscribe({
      next: (data) => {
        this.pedidos.set(Array.isArray(data) ? data : []);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  deletarPedido(pedido: any): void {
    this.confirmationService.confirm({
      message: `Tem certeza que deseja excluir o pedido de <b>${pedido.nomeCliente}</b>? Esta ação não pode ser desfeita.`,
      header: 'Confirmar Exclusão',
      icon: 'pi pi-trash',
      acceptLabel: 'Sim, Excluir',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-danger p-button-text',
      rejectButtonStyleClass: 'p-button-secondary p-button-text',
      accept: () => {
        this.executeDelete(pedido.id);
      }
    });
  }

  private executeDelete(id: string): void {
    this.apiService.deletarPedido(id).subscribe({
      next: () => {
        this.messageService.add({ 
          severity: 'success', 
          summary: 'Excluído', 
          detail: 'O pedido foi removido com sucesso.' 
        });
        this.carregarPedidos();
      },
      error: () => {
        this.messageService.add({ 
          severity: 'error', 
          summary: 'Erro', 
          detail: 'Não foi possível excluir o pedido.' 
        });
      }
    });
  }

  novoPedido() { this.router.navigate(['/pedidos/novo']); }
  editarPedido(id: string) { this.router.navigate(['/pedidos', id, 'editar']); }
}