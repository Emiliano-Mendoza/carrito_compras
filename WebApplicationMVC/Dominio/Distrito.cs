using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Distrito
    {
        [Key]
        public int IdDistrito { get; set; }
        public string Descripcion { get; set; }
    }
}
