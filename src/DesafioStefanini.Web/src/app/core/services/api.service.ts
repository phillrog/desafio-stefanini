import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CreatePedidoRequest, PedidoResponse, ProdutoResponse, Result, UpdatePedidoRequest } from '../../shared/models/pedido.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  getPedidos(search?: string): Observable<PedidoResponse[]> {
    let params = new HttpParams();
    if (search) {
      params = params.set('search', search);
    }
    return this.http.get<Result<PedidoResponse[]>>(`${this.apiUrl}/Pedido`, { params })
                    .pipe(map(res => res.data));
  }

  getPedidoById(id: string): Observable<PedidoResponse> {
    return this.http.get<Result<PedidoResponse>>(`${this.apiUrl}/Pedido/${id}`)
                    .pipe(map(res => res.data));
  }

  createPedido(pedido: CreatePedidoRequest): Observable<PedidoResponse> {
    return this.http.post<Result<PedidoResponse>>(`${this.apiUrl}/Pedido`, pedido)
                    .pipe(map(res => res.data));
  }

  updatePedido(id: string, pedido: UpdatePedidoRequest): Observable<PedidoResponse> {
    return this.http.put<Result<PedidoResponse>>(`${this.apiUrl}/Pedido/${id}`, pedido)
                    .pipe(map(res => res.data));
  }

  deletarPedido(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Pedido/${id}`);
  }

  getProdutos(): Observable<ProdutoResponse[]> {    
    return this.http.get<Result<ProdutoResponse[]>>(`${this.apiUrl}/Produto`)
                    .pipe(map(res => res.data));
  }
}
