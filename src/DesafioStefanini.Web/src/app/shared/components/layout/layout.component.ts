import { Component, OnInit } from '@angular/core';
import { LoadingService } from '../../../core/services/loading.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  standalone: false
})
export class LayoutComponent implements OnInit {
  sidebarVisible = false;

  constructor(public loadingService: LoadingService) {}

  ngOnInit(): void {}
}
