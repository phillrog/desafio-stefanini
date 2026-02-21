import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// PrimeNG
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { SkeletonModule } from 'primeng/skeleton';
import { ToastModule } from 'primeng/toast';
import { CardModule } from 'primeng/card';
import { SelectModule } from 'primeng/select'; // PrimeNG v18+ uses Select instead of Dropdown
import { DrawerModule } from 'primeng/drawer'; // Sidebar is now Drawer
import { ToolbarModule } from 'primeng/toolbar';
import { ProgressBarModule } from 'primeng/progressbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessageModule } from 'primeng/message';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { DividerModule } from 'primeng/divider';
import { TooltipModule } from 'primeng/tooltip';

import { LayoutComponent } from './components/layout/layout.component';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { TagModule } from 'primeng/tag';

import { AvatarModule } from 'primeng/avatar';
import { Avatar } from 'primeng/avatar'; 
import { AvatarGroup } from 'primeng/avatargroup';
import { ChartModule } from 'primeng/chart';


@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToolbarModule,
    ButtonModule,
    DrawerModule,
    ProgressBarModule,
    ToastModule,
    

    AvatarModule,   
    Avatar,     
    AvatarGroup,
  ],
  exports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    DialogModule,
    SkeletonModule,
    ToastModule,
    CardModule,
    SelectModule,
    DrawerModule,
    ToolbarModule,
    ProgressBarModule,
    ConfirmDialogModule,
    MessageModule,
    InputNumberModule,
    CheckboxModule,
    DividerModule,
    TooltipModule,
    LayoutComponent,
    
    IconFieldModule, 
    InputIconModule, 
    TagModule,       

    AvatarModule,   
    Avatar,     
    AvatarGroup,
    ChartModule
  ]
})
export class SharedModule { }
