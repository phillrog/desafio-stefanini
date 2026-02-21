using DesafioStefanini.Infrastructure.Configs;
using DesafioStefanini.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Configuração Segregadas
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddCustomizedSwagger(builder.Configuration, typeof(Program));

var app = builder.Build();

// Seeds
await app.UseDbInitializationAsync();


app.UseCustomizedSwagger();
app.UseRouting();
app.MapControllers();

app.Run();