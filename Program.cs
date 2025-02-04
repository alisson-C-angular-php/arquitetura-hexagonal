using crudBack.Repository;
using crudBack;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar a Configuration no DI como Singleton
builder.Services.AddSingleton<Configuration>();

// Registrar o AlunoRepository no DI como Scoped
builder.Services.AddScoped<AlunoRepository>();

var app = builder.Build();

// Configurar o pipeline de requisi��es HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
