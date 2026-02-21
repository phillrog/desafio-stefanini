import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'pedidos',
        pathMatch: 'full'
      },
      {
        path: 'pedidos',
        loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'pedidos/novo',
        loadChildren: () => import('./features/pedido-form/pedido-form.module').then(m => m.PedidoFormModule)
      },
      {
        path: 'pedidos/:id/editar',
        loadChildren: () => import('./features/pedido-form/pedido-form.module').then(m => m.PedidoFormModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule)
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'pedidos'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
