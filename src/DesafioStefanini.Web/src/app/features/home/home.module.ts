import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { PedidosListComponent } from './pedidos-list.component';

const routes: Routes = [
  { path: '', component: PedidosListComponent }
];

@NgModule({
  declarations: [
    PedidosListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class HomeModule { }
