using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Infrastructure.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Produtos.AnyAsync()) return;

        // Cria a Lista de 15+ Produtos
        var produtos = new List<Produto>
        {
            new("Mouse Gamer RGB", 150.00m),
            new("Teclado Mecânico", 350.50m),
            new("Monitor 24 LED HD", 899.90m),
            new("Monitor 27 Curvo", 1599.00m),
            new("Headset 7.1 Surr", 180.00m),
            new("Webcam 1080p Full", 210.00m),
            new("Mousepad Extra G", 85.90m),
            new("Cadeira Gamer", 1250.00m),
            new("SSD 1TB NVMe M2", 450.00m),     
            new("Memória RAM 16GB", 320.00m), 
            new("Processador Core i7", 1850.00m),
            new("Placa Vídeo RTX4060", 2450.00m),
            new("Gabinete ATX Vidro", 380.00m),
            new("Fonte 650W Gold", 520.00m),
            new("Cooler Air RGB", 145.00m),   
            new("Roteador Wi-Fi 6", 430.00m),
            new("Suporte Monitor", 150.00m),   
            new("Microfone USB Cond", 295.00m),
            new("Hub USB 3.0 Portas", 89.00m),
            new("Cabo HDMI 2.0 2m", 45.00m)
        };

        // Cria os Pedidos Iniciais
        var pedido1 = new Pedido("Avaliador Stefanini", "avaliador@stefanini.com", pago: true);
        var pedido2 = new Pedido("Phillipe Roger", "phill@candidato.com", pago: false);
        var pedido3 = new Pedido("João Silva", "joao@email.com", pago: false);

        // Adiciona os Itens
        pedido1.AdicionarItem(produtos[0], 2);  // 2 Mouses
        pedido1.AdicionarItem(produtos[2], 1);  // 1 Monitor 24

        pedido2.AdicionarItem(produtos[1], 1);  // 1 Teclado
        pedido2.AdicionarItem(produtos[4], 1);  // 1 Headset

        pedido3.AdicionarItem(produtos[11], 1); // 1 Placa de Vídeo
        pedido3.AdicionarItem(produtos[13], 1); // 1 Fonte

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Produtos.AddRangeAsync(produtos);
                await context.Pedidos.AddRangeAsync(new List<Pedido> { pedido1, pedido2, pedido3 });

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

    }
}