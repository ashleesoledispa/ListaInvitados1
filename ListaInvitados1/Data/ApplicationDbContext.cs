using Microsoft.EntityFrameworkCore;
using ListaInvitados1.Models;

namespace ListaInvitados1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        public DbSet<Invitado> Invitados { get; set; }
    }
}
