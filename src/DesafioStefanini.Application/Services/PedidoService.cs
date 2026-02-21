using AutoMapper;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Interfaces.Services;
using DesafioStefanini.Domain.Common.Models;
using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioStefanini.Application.Services;

public class PedidoService : IPedidoService
{
    #region [ VARIÁVEIS PRIVADAS ]

    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreatePedidoRequest> _createValidator;
    private readonly IValidator<UpdatePedidoRequest> _updateValidator;

    #endregion

    #region [ MÉTODO CONSTRUTOR ]

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IProdutoRepository produtoRepository,
        IMapper mapper,
        IValidator<CreatePedidoRequest> createValidator,
        IValidator<UpdatePedidoRequest> updateValidator)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    #endregion

    #region [ MÉTODOS DE BUSCAS ]

    public async Task<Result<IEnumerable<PedidoResponse>>> GetAllAsync()
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<PedidoResponse>>(pedidos);

        return Result<IEnumerable<PedidoResponse>>.Success(response);
    }

    public async Task<Result<PedidoResponse>> GetByIdAsync(int id)
    {
        var pedido = await _pedidoRepository.GetPedidoCompletoAsync(id);

        if (pedido == null)
            return Result<PedidoResponse>.Failure("Pedido não encontrado.");

        return Result<PedidoResponse>.Success(_mapper.Map<PedidoResponse>(pedido));
    }

    #endregion

    #region [ CADASTRAR ]

    public async Task<Result<PedidoResponse>> CreateAsync(CreatePedidoRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result<PedidoResponse>.Failure(validationResult.Errors.Select(e => e.ErrorMessage));

        var pedido = new Pedido(request.NomeCliente, request.EmailCliente, pago: false);

        foreach (var itemReq in request.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemReq.IdProduto);
            if (produto == null)
                return Result<PedidoResponse>.Failure($"Produto {itemReq.IdProduto} não encontrado.");

            var resultItem = pedido.AdicionarItem(produto, itemReq.Quantidade);

            if (!resultItem.IsSuccess)
                return Result<PedidoResponse>.Failure(resultItem.Errors);
        }

        await _pedidoRepository.CreateAsync(pedido);
        await _pedidoRepository.SaveChangesAsync();

        return Result<PedidoResponse>.Success(_mapper.Map<PedidoResponse>(pedido));
    }

    #endregion

    #region [ ALTERAR ]

    public async Task<Result<PedidoResponse>> UpdateAsync(UpdatePedidoRequest request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result<PedidoResponse>.Failure(validationResult.Errors.Select(e => e.ErrorMessage));

        var pedido = await _pedidoRepository.GetPedidoCompletoAsync(request.Id);
        if (pedido == null)
            return Result<PedidoResponse>.Failure("Pedido não encontrado.");

        var resultDados = pedido.AtualizarDados(request.NomeCliente, request.EmailCliente, request.Pago);
        if (!resultDados.IsSuccess)
            return Result<PedidoResponse>.Failure(resultDados.Errors);

        var novosItens = new List<ItemPedido>();
        foreach (var itemReq in request.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemReq.IdProduto);
            if (produto == null)
                return Result<PedidoResponse>.Failure($"Produto {itemReq.IdProduto} não encontrado.");

            var resultItem = ItemPedido.Criar(produto, itemReq.Quantidade);
            if (!resultItem.IsSuccess)
                return Result<PedidoResponse>.Failure(resultItem.Errors);

            novosItens.Add(resultItem.Data!);
        }

        var resultItens = pedido.AtualizarItens(novosItens);
        if (!resultItens.IsSuccess)
            return Result<PedidoResponse>.Failure(resultItens.Errors);

        _pedidoRepository.Update(pedido);
        await _pedidoRepository.SaveChangesAsync();

        return Result<PedidoResponse>.Success(_mapper.Map<PedidoResponse>(pedido));
    }

    #endregion

    #region [ DELETAR ]

    public async Task<Result> DeleteAsync(int id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido == null)
            return Result.Failure("Pedido não encontrado.");

        if (pedido.Pago)
            return Result.Failure("Não é possível excluir um pedido que já foi pago.");

        _pedidoRepository.Delete(pedido);
        await _pedidoRepository.SaveChangesAsync();

        return Result.Success();
    }

    #endregion
}