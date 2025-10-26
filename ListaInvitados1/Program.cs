using ListaInvitados1.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// CONFIGURACI�N DE SERVICIOS
// =======================================
builder.Services.AddControllersWithViews();

// Base de datos SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// =======================================
// CONSTRUCCI�N DE LA APP
// =======================================
var app = builder.Build();

// =======================================
// CONFIGURACI�N DEL PIPELINE HTTP
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
