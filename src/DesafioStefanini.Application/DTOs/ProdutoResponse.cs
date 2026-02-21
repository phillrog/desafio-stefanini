namespace DesafioStefanini.Application.DTOs
{
    /// <summary>
    /// Representa os dados de um produto para exibição em listagens.
    /// </summary>
    /// <param name="Id">Identificador único do produto.</param>
    /// <param name="NomeProduto">Nome descritivo do produto.</param>
    /// <param name="Valor">Preço unitário atual do produto.</param>
    public record ProdutoResponse(
        int Id,
        string NomeProduto,
        decimal Valor
    );
}
