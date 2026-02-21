import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PedidoFormComponent } from './pedido-form.component';
import { SharedModule } from '../../shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

describe('PedidoFormComponent', () => {
  let component: PedidoFormComponent;
  let fixture: ComponentFixture<PedidoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidoFormComponent ],
      imports: [ SharedModule, HttpClientTestingModule, NoopAnimationsModule, RouterTestingModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PedidoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
