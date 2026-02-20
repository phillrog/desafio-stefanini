namespace DesafioStefanini.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string NomeProduto { get; private set; }
        public decimal Valor { get; private set; }

        protected Produto() { } // EF Core

        public Produto(string nomeProduto, decimal valor)
        {
            NomeProduto = nomeProduto;
            Valor = valor;
        }
    }
}
