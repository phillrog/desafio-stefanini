using DesafioStefanini.Domain.Common.Models;

namespace DesafioStefanini.Domain.Entities
{
    public class ItemPedido : EntityBase
    {
        public int IdPedido { get; private set; }
        public int IdProduto { get; private set; }
        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public virtual Pedido Pedido { get; private set; }
        public virtual Produto Produto { get; private set; }

        protected ItemPedido() { }

        public ItemPedido(Produto produto, int quantidade)
        {
            IdProduto = produto.Id;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = produto.Valor;
        }

        public decimal CalcularValorItem() => ValorUnitario * Quantidade;

        public static Result<ItemPedido> Criar(Produto produto, int quantidade)
        {
            if (produto == null)
                return Result<ItemPedido>.Failure("O produto é obrigatório.");

            if (quantidade <= 0)
                return Result<ItemPedido>.Failure("A quantidade deve ser maior que zero.");

            return Result<ItemPedido>.Success(new ItemPedido(produto, quantidade));
        }
    }
}
