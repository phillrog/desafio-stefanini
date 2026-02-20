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
    }
}
