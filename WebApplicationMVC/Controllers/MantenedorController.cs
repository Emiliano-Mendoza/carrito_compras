using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Dominio;
using WebApplicationMVC.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace WebApplicationMVC.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly DataContext _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public MantenedorController(DataContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        #region CATEGORIA
        public IActionResult Categoria()
        {
            return View();
        }

        public async Task<JsonResult> ListarCategorias()
        {
            
            List<Categoria> categorias = new List<Categoria>();
            categorias = await _context.Categorias.ToListAsync();


            return Json(new { data = categorias });

        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria([FromBody] Categoria objeto)
        {
            bool resultado = false;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categorias.Add(objeto);
                    await _context.SaveChangesAsync();

                    resultado = true;

                    return StatusCode(StatusCodes.Status201Created,
                    Json(new { resultado = resultado, categoria = objeto, mensaje = "Categoria creada correctamente." }));


                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al crear la categoria.");
                }

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new {resultado = resultado ,categoria = objeto, mensaje = ModelState }));
            }

        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> EditarCategoria([FromBody] Categoria objeto)
        {
            bool resultado = false;

            if (ModelState.IsValid)
            {
                try
                {
                    Categoria categoria = await _context.Categorias.FindAsync(objeto.IdCategoria);
                    if(categoria == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            Json(new { resultado = resultado, mensaje = $"Categoria con Id = {objeto.IdCategoria} no encontrada." }));
                    }

                    _context.Entry(categoria).State = EntityState.Detached;
                    _context.Categorias.Update(objeto);
                    await _context.SaveChangesAsync();

                    resultado = true;

                    return StatusCode(StatusCodes.Status200OK,
                        Json(new { resultado = resultado, categoria = objeto, mensaje = "Categoria modificada correctamente." }));

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al editar la categoria.");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new { resultado = resultado, categoria = objeto, mensaje = ModelState }));
            }
            
        }

        [HttpDelete]
        public async Task<ActionResult<Categoria>> EliminarCategoria(int id)
        {
            bool resultado = false;

            try
            {
                Categoria categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        Json(new { resultado = resultado, mensaje = $"Categoria con Id = {id} no encontrada." }));
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                resultado = true;

                return StatusCode(StatusCodes.Status200OK,
                    Json(new{ resultado = resultado, mensaje = $"Categoria con Id = {id} eliminada correctamente."}));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error al eliminar la categoria.");
            }

        }
        #endregion CATEGORIA

        #region MARCA   

        public IActionResult Marca()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ListarMarcas()
        {
            List<Marca> marcas = new List<Marca>();
            marcas = await _context.Marcas.ToListAsync();

            return Json(new { data = marcas});
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> CrearMarca([FromBody] Marca marca)
        {
            bool resultado = false;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Marcas.Add(marca);
                    await _context.SaveChangesAsync();
                    resultado = true;

                    return StatusCode(StatusCodes.Status201Created,
                        Json(new { resultado = resultado, marca = marca, mensaje = "La marca se ha creado correctamente." }));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error al crear la marca.");
                }
                
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new { resultado = resultado, marca = marca, mensaje = ModelState }));
            }

        }

        [HttpPut]
        public async Task<ActionResult<Marca>> EditarMarca([FromBody] Marca objeto)
        {
            bool resultado = false;

            if(ModelState.IsValid)
            {
                try
                {
                    Marca marca = await _context.Marcas.FindAsync(objeto.IdMarca);
                    if(marca == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"Marca con Id={objeto.IdMarca} no encontada.");
                    }

                    _context.Entry(marca).State = EntityState.Detached;
                    _context.Marcas.Update(objeto);
                    await _context.SaveChangesAsync();

                    resultado = true;

                    return StatusCode(StatusCodes.Status200OK,
                        Json(new { resultado = resultado, marca = objeto, mensaje = "Marca modificada correctamente." }));


                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error al editar la marca.");
                }


            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new { resultado = resultado, marca = objeto, mensaje = ModelState }));
            }
 
        }

        [HttpDelete]
        public async Task<ActionResult<Marca>> EliminarMarca(int id)
        {
            bool resultado = false;

            try
            {
                Marca marca = await _context.Marcas.FindAsync(id);
                if (marca == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        Json(new { resultado = resultado, mensaje = $"Marca con Id = {id} no encontrada." }));
                }

                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();

                resultado = true;

                return StatusCode(StatusCodes.Status200OK,
                    Json(new { resultado = resultado, mensaje = $"Marca con Id = {id} eliminada correctamente." }));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error al eliminar la marca.");
            }
        }

        #endregion

        #region PRODUCTO
        public IActionResult Producto()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ListarProductos()
        {
            List<Producto> producto = new List<Producto>();
            producto = await _context.Productos.Include(p => p.oMarca).Include(p => p.oCategoria).ToListAsync();

            return Json(new { data = producto });
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto([FromBody] Producto producto)
        {
            bool resultado = false;

            if (ModelState.IsValid)
            {
                try
                {
                    Categoria categoria = await _context.Categorias.FindAsync(producto.oCategoria.IdCategoria);
                    Marca marca = await _context.Marcas.FindAsync(producto.oMarca.IdMarca);

                    if(marca == null || categoria == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            "No se pudo encontrar la marca o categoria a la que hacer referencia el producto.");
                    }


                    producto.oCategoria = categoria;
                    producto.oMarca = marca;

                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();
                    resultado = true;

                    return StatusCode(StatusCodes.Status201Created,
                        Json(new { resultado = resultado, producto = producto, mensaje = "El producto se ha creado correctamente." }));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error al crear el producto.");
                }

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new { resultado = resultado, producto = producto, mensaje = ModelState }));
            }

        }

        [HttpPut]
        public async Task<ActionResult<Producto>> EditarProducto([FromBody] Producto objeto)
        {
            bool resultado = false;

            if (ModelState.IsValid)
            {
                try
                {
                    Producto producto = await _context.Productos.
                                                Include(p => p.oMarca).
                                                Include(p => p.oCategoria).
                                                FirstOrDefaultAsync(i => i.IdProducto == objeto.IdProducto);

                    if (producto == default(Producto))
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            $"Producto con Id={objeto.IdProducto} no encontada.");
                    }

                    if (objeto.oCategoria.IdCategoria != producto.oCategoria.IdCategoria)
                    {
                        Categoria categoria = await _context.Categorias.FindAsync(objeto.oCategoria.IdCategoria);

                        if (categoria == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound,
                                "No se pudo encontrar la categoria a la que hacer referencia el producto.");
                        }
                        objeto.oCategoria = categoria;
                    }

                    if (objeto.oMarca.IdMarca != producto.oMarca.IdMarca)
                    {
                        Marca marca = await _context.Marcas.FindAsync(objeto.oMarca.IdMarca);

                        if (marca == null)
                        {
                            return StatusCode(StatusCodes.Status404NotFound,
                                "No se pudo encontrar la marca a la que hacer referencia el producto.");
                        }
                        objeto.oMarca = marca;
                    }

                    _context.Entry(producto).State = EntityState.Detached;
                    _context.Entry(producto.oMarca).State = EntityState.Detached;
                    _context.Entry(producto.oCategoria).State = EntityState.Detached;

                    _context.Productos.Update(objeto);
                    await _context.SaveChangesAsync();

                    resultado = true;

                    return StatusCode(StatusCodes.Status200OK,
                        Json(new { resultado = resultado, producto = objeto, mensaje = "Producto modificado correctamente." }));


                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error al editar el producto.");
                }


            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    Json(new { resultado = resultado, producto = objeto, mensaje = ModelState }));
            }

        }

        [HttpDelete]
        public async Task<ActionResult<Producto>> EliminarProducto(int id)
        {
            bool resultado = false;

            try
            {
                Producto producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        Json(new { resultado = resultado, mensaje = $"Producto con Id = {id} no encontrado." }));
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                resultado = true;

                return StatusCode(StatusCodes.Status200OK,
                    Json(new { resultado = resultado, mensaje = $"Producto con Id = {id} eliminado correctamente." }));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error al eliminar el producto.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Producto>> EditarProductoImagen(
                    [FromForm] string idProducto,
                    [FromForm] string nombre,
                    [FromForm] string descripcion,
                    [FromForm] int marca,
                    [FromForm] int categoria,
                    [FromForm] string precio,
                    [FromForm] string stock,
                    [FromForm] int activo,
                    IFormFile? imagen)
        {

            if (ModelState.IsValid)
            {              
                try
                {

                    Producto producto = await _context.Productos.
                                                Include(p => p.oMarca).
                                                Include(p => p.oCategoria).
                                                FirstOrDefaultAsync(i => i.IdProducto == Int32.Parse(idProducto));

                    if (producto == default(Producto))
                    {
                        return StatusCode(StatusCodes.Status404NotFound,
                            $"Producto con Id={idProducto} no encontado.");
                    }


                    producto.Nombre = nombre;
                    producto.Descripcion = descripcion;
                    if (activo == 1) producto.Activo = true;


                    Marca marcaAux = await _context.Marcas.FindAsync(marca);
                    if (marcaAux == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                                    Json(new { mensaje = $"No se encontró marca con id={marca}" }));
                    }

                    Categoria categoriaAux = await _context.Categorias.FindAsync(categoria);
                    if (categoriaAux == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                                    Json(new { mensaje = $"No se encontró categoría con id={categoria}" }));
                    }

                    producto.oMarca = marcaAux;
                    producto.oCategoria = categoriaAux;
                    producto.Stock = Int32.Parse(stock);

                    decimal value = decimal.Parse(precio, CultureInfo.InvariantCulture);

                    producto.Precio = Convert.ToDecimal(value);

                    if (imagen != null)
                    {
                        string wwwPath = this.Environment.WebRootPath;
                        string contentPath = this.Environment.ContentRootPath;

                        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string extension = Path.GetExtension(imagen.FileName);
                        string nombreImagen = string.Concat(producto.IdProducto.ToString(), extension);

                        producto.NombreImagen = nombreImagen;
                        producto.RutaImagen = path;

                        using (FileStream stream = new FileStream(Path.Combine(path, nombreImagen), FileMode.Create))
                        {
                            imagen.CopyTo(stream);

                        }

                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }


                ViewData["Mensaje"] = "Se editó el producto correctamente.";
                return View("Producto");

            }
            else
            {
                ViewData["MensajeError"] = "No se pudo editar el producto, procure completar todos los campos.";
                return View("Producto");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProductoImagen(
                    [FromForm] string nombre,
                    [FromForm] string descripcion,
                    [FromForm] int marca,
                    [FromForm] int categoria,
                    [FromForm] string precio,
                    [FromForm] string stock,
                    [FromForm] int activo,
                    IFormFile? imagen)
        { 
            
            if (ModelState.IsValid)
            {
                Producto producto = new Producto();
                producto.Nombre = nombre;
                producto.Descripcion = descripcion;
                if (activo == 1) producto.Activo = true;

                try
                {

                    Marca marcaAux = await _context.Marcas.FindAsync(marca);
                    if(marcaAux == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                                    Json(new { mensaje = $"No se encontró marca con id={marca}"}));
                    }

                    Categoria categoriaAux = await _context.Categorias.FindAsync(categoria);
                    if (categoriaAux == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                                    Json(new { mensaje = $"No se encontró categoría con id={categoria}" }));
                    }

                    producto.oMarca = marcaAux; 
                    producto.oCategoria = categoriaAux;
                    producto.Stock = Int32.Parse(stock);

                    decimal value = decimal.Parse(precio, CultureInfo.InvariantCulture);

                    producto.Precio = Convert.ToDecimal(value);

                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();

                    if(imagen != null)
                    {

                        string wwwPath = this.Environment.WebRootPath;
                        string contentPath = this.Environment.ContentRootPath;

                        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string extension = Path.GetExtension(imagen.FileName);
                        string nombreImagen = string.Concat(producto.IdProducto.ToString(), extension);

                        producto.NombreImagen = nombreImagen;
                        producto.RutaImagen = path;

                        using (FileStream stream = new FileStream(Path.Combine(path, nombreImagen), FileMode.Create))
                        {
                            imagen.CopyTo(stream);

                        }

                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();

                    }                   
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }


                ViewData["Mensaje"] = "Se creo el producto correctamente.";
                return View("Producto");

            }
            else
            {
                ViewData["MensajeError"] = "No se pudo crear el nuevo producto, procure completar todos los campos.";
                return View("Producto");
            }
            
        }

        [HttpPost]
        public async Task<JsonResult> ImagenProducto(int id)
        {
            bool conversion = false;


            try
            {
                Producto producto = await _context.Productos.FindAsync(id);

                if(producto == null)
                {
                    return Json(new
                    {
                        conversion = conversion,
                        mensaje = $"Producto con id={id} no encontrado"
                    });
                }

                if (producto.NombreImagen == null || producto.RutaImagen == null)
                {
                    return Json(new
                    {
                        conversion = conversion,
                        mensaje = $"No se pudo encontrar la imagen"
                    });
                }

                string textoBase64 = imageHelper.ConvertirBase64(Path.Combine(producto.RutaImagen, producto.NombreImagen), out conversion);

                return Json(new
                {
                    conversion = conversion,
                    textoBase64 = textoBase64,
                    extension = Path.GetExtension(producto.NombreImagen)
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    conversion = conversion,
                    mensaje = "No se pudo convertir la imagen"
                });
            }

            
        }

        #endregion
    }

}
