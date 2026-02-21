using FluentValidation;
using DesafioStefanini.Application.DTOs;

namespace DesafioStefanini.Application.Validators;

public class UpdatePedidoValidator : AbstractValidator<UpdatePedidoRequest>
{
    public UpdatePedidoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("ID do pedido é obrigatório para atualização.");
        RuleFor(x => x.NomeCliente).NotEmpty().MaximumLength(60);
        RuleFor(x => x.EmailCliente).NotEmpty().EmailAddress().MaximumLength(60);
        RuleFor(x => x.Itens).NotEmpty().WithMessage("O pedido deve ter pelo menos um item.");

        RuleForEach(x => x.Itens).ChildRules(item => {
            item.RuleFor(i => i.IdProduto).GreaterThan(0);
            item.RuleFor(i => i.Quantidade).GreaterThan(0);
        });
    }
}