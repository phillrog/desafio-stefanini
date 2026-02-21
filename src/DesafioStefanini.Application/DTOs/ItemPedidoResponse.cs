namespace DesafioStefanini.Application.DTOs
{
    public record ItemPedidoResponse(
    int Id,
    int IdProduto,
    string NomeProduto,
    int Quantidade,
    decimal ValorUnitario
);
}
