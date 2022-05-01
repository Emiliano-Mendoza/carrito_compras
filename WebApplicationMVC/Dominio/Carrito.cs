using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }
        public Cliente oCliente { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
