namespace WebApplicationMVC.Helpers;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Dominio;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
 
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }
    public DbSet<Distrito> Distritos { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public async Task<Usuario> GetUsuarioID1()
    {
        return await Usuarios.FirstAsync(x => x.IdUsuario == 1);
    }


}
