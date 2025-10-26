using ListaInvitados1.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL; // <-- importante

var builder = WebApplication.CreateBuilder(args);

// =======================================
// CONFIGURACIÓN DE SERVICIOS
// =======================================
builder.Services.AddControllersWithViews();

// Base de datos PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// =======================================
// CONSTRUCCIÓN DE LA APP
// =======================================
var app = builder.Build();

// =======================================
// CONFIGURACIÓN DEL PIPELINE HTTP
// =======================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Invitados}/{action=Index}/{id?}");

// Aplicar migraciones automáticas al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
