using crudBack;
using crudBack.Repository;
using crudBack.outbound.service; // Add this namespace to access RabbitMQService

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar a Configuration no DI como Singleton
builder.Services.AddSingleton<Configuration>();

// Registrar o AlunoRepository no DI como Scoped
builder.Services.AddScoped<AlunoRepository>();

// Registrar o RedisService no DI
builder.Services.AddSingleton<RedisService>(); // Assuming RedisService is a singleton or similar

// Registrar o RabbitMQService no DI como Scoped
builder.Services.AddScoped<RabbitMQService>(); // Add this line

var app = builder.Build();

// Configurar o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
