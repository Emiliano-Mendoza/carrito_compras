using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }
        public string Descripcion { get; set; }
    }
}
