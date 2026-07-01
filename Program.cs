var builder = WebApplication.CreateBuilder(args);

// 1. Registrar controladores
builder.Services.AddControllers();

// 2. Configurar la regla CORS forzada para evitar bloqueos del navegador
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirWeb", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. OBLIGATORIO: El middleware de CORS debe ir exactamente aquí, antes de la redirección y mapeo
app.UseCors("PermitirWeb");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 4. Mapear las rutas de los controladores
app.MapControllers();

app.Run();
