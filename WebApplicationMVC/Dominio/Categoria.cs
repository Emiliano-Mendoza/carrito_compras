using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
