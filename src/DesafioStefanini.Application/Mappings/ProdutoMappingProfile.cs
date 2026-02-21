using AutoMapper;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Domain.Entities;

namespace DesafioStefanini.Application.Mappings;

public class ProdutoMappingProfile : Profile
{
    public ProdutoMappingProfile()
    {
        // Mapeamento de Produto
        CreateMap<Produto, ProdutoResponse>();
    }
}