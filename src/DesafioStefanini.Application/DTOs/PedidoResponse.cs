namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Resposta detalhada do pedido (Modelo JSON 1 do desafio).
    /// </summary>
    public record PedidoResponse(
    int Id,
    string NomeCliente,
    string EmailCliente,
    bool Pago,
    decimal ValorTotal,
    IEnumerable<ItemPedidoResponse> ItensPedido
);
}
