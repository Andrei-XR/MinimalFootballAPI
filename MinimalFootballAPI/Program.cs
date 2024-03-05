using Microsoft.EntityFrameworkCore;
using MinimalFootballAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/clubes", async (DataContext context) =>
    await context.Clubes.ToListAsync());

app.MapGet("/clubes/{id}", async (DataContext context, int id) =>
{
    var clube = await context.Clubes.FindAsync(id);

    if (clube is null)
        return Results.NotFound("Clube não encontrado!");

    return Results.Ok(clube);
});

app.MapPost("/clubes/add", async (DataContext context, Clube clube) =>
{
    context.Clubes.Add(clube);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Clubes.ToListAsync());
});

app.MapPut("/clubes/edit/{id}", async (DataContext context, Clube updatedClube, int id) =>
{
    var clube = await context.Clubes.FindAsync(id);

    if (clube is null)
        return Results.NotFound("Clube não encontrado!");

    clube.Nome = updatedClube.Nome;
    clube.Fundacao = updatedClube.Fundacao;
    await context.SaveChangesAsync();

    return Results.Ok(await context.Clubes.ToListAsync());
});

app.MapDelete("/clubes/remove/{id}", async (DataContext context, int id) =>
{
    var clube = await context.Clubes.FindAsync(id);

    if (clube is null)
        return Results.NotFound("Clube não encontrado!");

    context.Clubes.Remove(clube);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Clubes.ToListAsync());
});

app.Run();

