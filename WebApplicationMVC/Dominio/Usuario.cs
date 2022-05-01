using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "El apellido debe tener como mínimo 3 carácteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}", ErrorMessage = "El correo es inválido")]
        public string Correo { get; set; }

        public string Clave { get; set; }
        public bool Reestablecer { get; set; }
        public bool Activo { get; set; }
    }
}
