using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListaInvitados1.Data;
using ListaInvitados1.Models;

namespace ListaInvitados1.Controllers
{
    public class InvitadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ========================================
        // LISTA PRINCIPAL
        // ========================================
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Invitados
                .OrderBy(i => i.Nombre)
                .ToListAsync();
            return View(lista);
        }

        // ========================================
        // IMPORTAR LISTA MASIVA
        // ========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Importar(string nombres)
        {
            if (!string.IsNullOrWhiteSpace(nombres))
            {
                // Separar cada línea y limpiar duplicados o espacios
                var lineas = nombres
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => n.Trim())
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                foreach (var nombre in lineas)
                {
                    // Evitar duplicados ya existentes
                    if (!_context.Invitados.Any(i => i.Nombre.ToLower() == nombre.ToLower()))
                    {
                        _context.Invitados.Add(new Invitado { Nombre = nombre });
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ========================================
        // CAMBIAR ESTADO DE REGISTRO (AJAX)
        // ========================================
        [HttpPost]
        public async Task<IActionResult> CambiarEstado([FromBody] CambiarEstadoRequest req)
        {
            if (req == null || req.Id <= 0)
                return BadRequest();

            var invitado = await _context.Invitados.FindAsync(req.Id);
            if (invitado == null)
                return NotFound();

            invitado.Registrado = req.Registrado;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Clase auxiliar para recibir JSON del fetch()
        public class CambiarEstadoRequest
        {
            public int Id { get; set; }
            public bool Registrado { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarTodos()
        {
            _context.Invitados.RemoveRange(_context.Invitados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
