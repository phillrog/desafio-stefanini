namespace DesafioStefanini.Application.DTOs
{
    public record PedidoResponse(
    int Id,
    string NomeCliente,
    string EmailCliente,
    bool Pago,
    decimal ValorTotal,
    IEnumerable<ItemPedidoResponse> ItensPedido
);
}
