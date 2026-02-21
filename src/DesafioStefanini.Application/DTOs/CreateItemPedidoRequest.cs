namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Representa o item a ser adicionado ao pedido.
    /// </summary>
    /// <param name="IdProduto">ID único do produto.</param>
    /// <param name="Quantidade">Quantidade desejada (deve ser maior que zero).</param>
    public record CreateItemPedidoRequest(int IdProduto, int Quantidade);
}
