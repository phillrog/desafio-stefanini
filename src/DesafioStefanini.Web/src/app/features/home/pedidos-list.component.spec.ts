import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PedidosListComponent } from './pedidos-list.component';
import { SharedModule } from '../../shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('PedidosListComponent', () => {
  let component: PedidosListComponent;
  let fixture: ComponentFixture<PedidosListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidosListComponent ],
      imports: [ SharedModule, HttpClientTestingModule, NoopAnimationsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PedidosListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
