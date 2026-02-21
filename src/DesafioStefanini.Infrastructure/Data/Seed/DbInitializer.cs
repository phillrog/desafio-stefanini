using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Infrastructure.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Pedidos.AnyAsync()) return;

        // 1. Criar Produtos (necessários para os itens de pedido)
        var mouse = new Produto("Mouse Gamer", 150.00m);
        var teclado = new Produto("Teclado Mecânico", 350.50m);
        var monitor = new Produto("Monitor 24' LED", 899.90m);

        var produtos = new List<Produto> { mouse, teclado, monitor };

        // 2. Criar Pedidos
        var pedido1 = new Pedido("Avaliador Stefanini", "avaliador@stefanini.com", pago: true);
        var pedido2 = new Pedido("Phillipe Roger", "phill@candidato.com", pago: false);

        // 3. Adicionar Itens aos Pedidos (Usando o método de domínio)
        // Pedido 1: 1 Mouse + 1 Monitor
        pedido1.AdicionarItem(mouse, 1);
        pedido1.AdicionarItem(monitor, 1);

        // Pedido 2: 2 Teclados
        pedido2.AdicionarItem(teclado, 2);

        // Garante que o banco existe e as migrations foram aplicadas
        await context.Database.MigrateAsync();

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Produtos.AddRangeAsync(produtos);
                await context.Pedidos.AddAsync(pedido1);
                await context.Pedidos.AddAsync(pedido2);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // Repassa para o logger no DependencyInjection
            }
        });
    }
}