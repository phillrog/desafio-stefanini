namespace DesafioStefanini.Domain.Entities
{
    public class Pedido : EntityBase
    {
        public string NomeCliente { get; private set; }
        public string EmailCliente { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Pago { get; private set; }

        private readonly List<ItemPedido> _itensPedido = new();
        public virtual IReadOnlyCollection<ItemPedido> ItensPedido => _itensPedido;

        public decimal ValorTotal => _itensPedido.Sum(x => x.Quantidade * x.ValorUnitario);

        protected Pedido() { }

        public Pedido(string nomeCliente, string emailCliente, bool pago)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            DataCriacao = DateTime.Now;
            Pago = pago;
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            _itensPedido.Add(new ItemPedido(produto, quantidade));
        }

        public void MarcarComoPago()
        {
            Pago = true;
        }
    }
}