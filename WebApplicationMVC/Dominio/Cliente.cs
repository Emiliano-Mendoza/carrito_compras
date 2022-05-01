using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Reestablecer { get; set; }
    }
}
