namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Solicitação para criação de um novo pedido.
    /// </summary>
    /// <param name="NomeCliente">Nome completo do cliente.</param>
    /// <param name="EmailCliente">E-mail de contato do cliente.</param>
    /// <param name="Itens">Lista de itens que compõem o pedido.</param>
    public record CreatePedidoRequest(
    string NomeCliente,
    string EmailCliente,
    List<CreateItemPedidoRequest> Itens
);
}
