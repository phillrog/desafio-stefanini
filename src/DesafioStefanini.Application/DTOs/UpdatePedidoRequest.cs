namespace DesafioStefanini.Application.DTOs
{
    public record UpdatePedidoRequest(
    int Id,
    string NomeCliente,
    string EmailCliente,
    bool Pago,
    List<UpdateItemPedidoRequest> Itens
);
}
