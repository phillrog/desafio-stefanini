namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Dados para atualização de um item no pedido.
    /// </summary>
    public record UpdateItemPedidoRequest(int IdProduto, int Quantidade);
}
