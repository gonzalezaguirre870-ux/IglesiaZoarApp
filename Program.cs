var builder = WebApplication.CreateBuilder(args);

// 1. Registrar controladores
builder.Services.AddControllers();

// 2. Configurar la regla CORS tal como lo propusiste
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAcceso", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. ESTRICTAMENTE EN ESTE ORDEN: Primero CORS, luego Autorización y Controladores
app.UseCors("PermitirAcceso");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
