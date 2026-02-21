using DesafioStefanini.Domain.Common.Models;

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

        public Result AdicionarItem(Produto produto, int quantidade)
        {
            // O AdicionarItem utiliza a Factory do ItemPedido para validar
            var resultItem = ItemPedido.Criar(produto, quantidade);

            if (!resultItem.IsSuccess)
                return Result.Failure(resultItem.Errors);

            _itensPedido.Add(resultItem.Data!);
            return Result.Success();
        }

        public Result MarcarComoPago()
        {
            Pago = true;
            return Result.Success();
        }

        public Result AtualizarDados(string nomeCliente, string emailCliente, bool pago)
        {
            if (Pago && !pago)
                return Result.Failure("Não é possível reverter um pedido pago para não pago.");

            if (Pago)
                return Result.Failure("Este pedido já foi finalizado/pago e não permite alterações.");

            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            Pago = pago;

            return Result.Success();
        }

        public Result AtualizarItens(List<ItemPedido> novosItens)
        {
            if (Pago)
                return Result.Failure("Não é possível alterar itens de um pedido que já foi pago.");

            _itensPedido.Clear();
            _itensPedido.AddRange(novosItens);

            return Result.Success();
        }
    }
}