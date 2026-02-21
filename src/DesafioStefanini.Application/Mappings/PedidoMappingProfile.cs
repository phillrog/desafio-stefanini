using AutoMapper;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Domain.Entities;

namespace DesafioStefanini.Application.Mappings;

public class PedidoMappingProfile : Profile
{
    public PedidoMappingProfile()
    {
        // Mapeamento de Pedido
        CreateMap<ItemPedido, ItemPedidoResponse>()
            .ConstructUsing(src => new ItemPedidoResponse(
                src.Id,
                src.Produto.Id,
                src.Produto.NomeProduto,
                src.Quantidade,
                src.ValorUnitario
            ));

        // Mapeamento de Pedido para PedidoResponse
        CreateMap<Pedido, PedidoResponse>()
            .ConstructUsing((src, ctx) => new PedidoResponse(
                src.Id,
                src.NomeCliente,
                src.EmailCliente,
                src.Pago,
                src.ValorTotal,
                ctx.Mapper.Map<IEnumerable<ItemPedidoResponse>>(src.ItensPedido)
            ));
    }
}