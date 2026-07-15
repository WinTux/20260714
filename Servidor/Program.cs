using Microsoft.EntityFrameworkCore;
using Servidor.Contenido;
using Servidor.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MiConexionFabulosaSQLite")));
var app = builder.Build();
app.MapGet("api/v1/plato", async (AppDbContext db) =>
{
    var platos = await db.Platos.ToListAsync();
    return Results.Ok(platos);
});
app.MapPost("api/v1/plato", async (AppDbContext db, Plato plato) =>
{
    db.Platos.Add(plato);
    await db.SaveChangesAsync();
    return Results.Created($"/api/v1/plato/{plato.Id}", plato);
});
app.MapPut("api/v1/plato/{id}", async (AppDbContext db, int id, Plato plato) =>
{
    var platoExistente = await db.Platos.FindAsync(id);
    if (platoExistente == null)
    {
        return Results.NotFound();
    }
    platoExistente.Nombre = plato.Nombre;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("api/v1/plato/{id}", async (AppDbContext db, int id) =>
{
    var plato = await db.Platos.FindAsync(id);
    if (plato == null)
    {
        return Results.NotFound();
    }
    db.Platos.Remove(plato);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();