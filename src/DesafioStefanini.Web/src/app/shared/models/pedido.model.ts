export interface ProdutoResponse {
  id: string;
  nome: string;
  valor: number;
}

export interface ItemPedidoResponse {
  id: string;
  idProduto: string;
  nomeProduto: string;
  quantidade: number;
  valorUnitario: number;
  valorTotal: number;
}

export interface PedidoResponse {
  id: string;
  nomeCliente: string;
  emailCliente: string;
  pago: boolean;
  itensPedido: ItemPedidoResponse[];
  valorTotal: number;
}

export interface CreatePedidoRequest {
  nomeCliente: string;
  emailCliente: string;
  pago: boolean;
  itens: {
    idProduto: string;
    quantidade: number;
  }[];
}

export interface UpdatePedidoRequest {
  id: string;
  nomeCliente: string;
  emailCliente: string;
  pago: boolean;
  itens: {
    idProduto: string;
    quantidade: number;
  }[];
}

export interface Result<T> {
  isSuccess: boolean;
  data: T;
  errors: string[];
}