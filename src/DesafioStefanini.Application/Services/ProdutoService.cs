using AutoMapper;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Interfaces.Services;
using DesafioStefanini.Domain.Common.Models;
using DesafioStefanini.Domain.Interfaces.Repositories;

namespace DesafioStefanini.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        #region [ VARIÁVEIS PRIVADAS ]

        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region [ MÉTODO CONSTRUTOR ]
        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        #endregion

        #region [ MÉTODO DE BUSCAS ]
        public async Task<Result<IEnumerable<ProdutoResponse>>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ProdutoResponse>>(produtos);

            return Result<IEnumerable<ProdutoResponse>>.Success(response);
        }
        #endregion
    }
}
