namespace DesafioStefanini.Application.DTOs
{
    public record CreatePedidoRequest(
    string NomeCliente,
    string EmailCliente,
    List<CreateItemPedidoRequest> Itens
);
}
