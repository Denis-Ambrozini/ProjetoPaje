using Microsoft.EntityFrameworkCore;
using ProjetoPaje.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar o DbContext para usar SQLite em memória
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlite("DataSource=file::memory:?cache=shared"));

var app = builder.Build();

// Garante que o banco de dados seja criado para SQLite em memória
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BancoContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

