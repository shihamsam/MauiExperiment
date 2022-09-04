using Microsoft.EntityFrameworkCore;
using TodoApi.DataContext;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=Todo.db"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/todo", (AppDbContext db) =>
{
    return Results.Ok(db.Todos.ToList());
});

app.MapPost("/api/todo", async (AppDbContext db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"todo/{todo.Id}", todo);
});

app.MapPut("/api/todo/{id}", async (AppDbContext db, Todo todo) =>
{
    var dbTodo = db.Todos.FirstOrDefault(x => x.Id == todo.Id);
    if (dbTodo == null) return Results.NotFound();

    dbTodo.TodoName = todo.TodoName;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapGet("/api/todo/{id}", (AppDbContext db, int id) =>
{
    var dbTodo = db.Todos.Find(id);

    return dbTodo == null ? Results.NotFound() : Results.Ok(dbTodo);
});

app.MapDelete("/api/todo/{id}", async (AppDbContext db, int id) =>
{
    var dbTodo = db.Todos.Find(id);
    if (dbTodo == null) return Results.NotFound();

    db.Todos.Remove(dbTodo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.Run();