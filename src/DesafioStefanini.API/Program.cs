using DesafioStefanini.Infrastructure.Configs;
using DesafioStefanini.Infrastructure.Seed;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração Segregadas
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

// Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Ignora propriedades com valor nulo no JSON de saída
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // Aceita nomes de propriedades sem diferenciar maiúsculas/minúsculas no POST/PUT
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

    // Garante que o JSON de saída use camelCase (padrão de mercado)
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddEndpointsApiExplorer();

// Cors
builder.Services.AddCors(options => {
    // Permite tudo
    options.AddPolicy("Cors", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddCustomizedSwagger(builder.Configuration, typeof(Program));

var app = builder.Build();

// Seeds
await app.UseDbInitializationAsync();

app.UseCors("Cors");

app.UseCustomizedSwagger();
app.UseRouting();
app.MapControllers();

app.Run();