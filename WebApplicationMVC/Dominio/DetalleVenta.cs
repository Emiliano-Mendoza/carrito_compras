using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Dominio
{
    public class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string IdTransaccion { get; set; }
    }
}
