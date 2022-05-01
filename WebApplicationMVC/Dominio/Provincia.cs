using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public string Descripcion { get; set; }
    }
}
