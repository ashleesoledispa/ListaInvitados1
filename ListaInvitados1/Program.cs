using ListaInvitados1.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// CONFIGURACIÓN DE SERVICIOS
// =======================================
builder.Services.AddControllersWithViews();

// Base de datos SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Ruta por defecto: abre Invitados/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Invitados}/{action=Index}/{id?}");

app.Run();
