import { Component, OnInit, computed, signal } from '@angular/core';
import { ApiService } from '../../core/services/api.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  standalone: false
})
export class DashboardComponent implements OnInit {
  pedidos = signal<any[]>([]);
  loading = signal(true);

  // KPI Total de Pedidos Pagos vs Pendentes
  kpis = computed(() => {
    const lista = this.pedidos();
    return {
      pagos: lista.filter(p => p.pago).length,
      pendentes: lista.filter(p => !p.pago).length,
      totalVendido: lista.filter(p => p.pago).reduce((acc, p) => acc + (Number(p.valorTotal) || 0), 0)
    };
  });

  // Gráfico de Produtos (Top Vendidos)
  chartData = computed(() => {
    const lista = this.pedidos();
    const produtosMap = new Map();

    lista.forEach(p => {
      p.itensPedido?.forEach((item: any) => {
        const atual = produtosMap.get(item.nomeProduto) || 0;
        produtosMap.set(item.nomeProduto, atual + item.quantidade);
      });
    });

    return {
      labels: Array.from(produtosMap.keys()),
      datasets: [{
        label: 'Quantidade Vendida',
        data: Array.from(produtosMap.values()),
        backgroundColor: ['#6366F1', '#F59E0B', '#10B981', '#EF4444', '#EC4899'],
        borderWidth: 1
      }]
    };
  });

  // Lista de Clientes com Variação (Pago/Pendente)
  clientesResumo = computed(() => {
    const lista = this.pedidos();
    const resumo = new Map();

    lista.forEach(p => {
      if (!resumo.has(p.nomeCliente)) {
        resumo.set(p.nomeCliente, { nome: p.nomeCliente, pago: 0, pendente: 0 });
      }
      const cli = resumo.get(p.nomeCliente);
      if (p.pago) cli.pago += Number(p.valorTotal);
      else cli.pendente += Number(p.valorTotal);
    });

    return Array.from(resumo.values());
  });

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getPedidos().subscribe(data => {
      this.pedidos.set(data);
      this.loading.set(false);
    });
  }
}