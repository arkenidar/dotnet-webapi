using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDb>(o => o.UseSqlite("Data Source=app.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/items", async (AppDb db) =>
    await db.Items.ToListAsync());

app.MapGet("/items/{id}", async (int id, AppDb db) =>
    await db.Items.FindAsync(id) is Item item
        ? Results.Ok(item)
        : Results.NotFound());

app.MapPost("/items", async (Item item, AppDb db) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.Id}", item);
});

app.Run();

record Item(int Id, string Name);

class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options) { }
    public DbSet<Item> Items => Set<Item>();
}