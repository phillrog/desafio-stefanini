namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Detalhes de um item dentro de um pedido existente.
    /// </summary>
    public record ItemPedidoResponse(
    int Id,
    int IdProduto,
    string NomeProduto,
    int Quantidade,
    decimal ValorUnitario
);
}
