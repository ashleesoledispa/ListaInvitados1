using System.ComponentModel.DataAnnotations;

namespace ListaInvitados1.Models
{
    public class Invitado
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Display(Name = "Registrado")]
        public bool Registrado { get; set; } = false;
    }
}
