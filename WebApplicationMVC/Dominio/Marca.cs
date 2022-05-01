using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
