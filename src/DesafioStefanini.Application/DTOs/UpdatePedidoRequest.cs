namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Solicitação para atualizar dados de um pedido existente.
    /// </summary>
    public record UpdatePedidoRequest(
    int Id,
    string NomeCliente,
    string EmailCliente,
    bool Pago,
    List<UpdateItemPedidoRequest> Itens
);
}
