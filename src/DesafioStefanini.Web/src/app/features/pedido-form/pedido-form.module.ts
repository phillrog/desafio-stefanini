import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { PedidoFormComponent } from './pedido-form.component';

const routes: Routes = [
  { path: '', component: PedidoFormComponent }
];

@NgModule({
  declarations: [
    PedidoFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class PedidoFormModule { }
