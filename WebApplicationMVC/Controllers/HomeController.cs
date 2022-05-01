using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplicationMVC.Helpers;
using WebApplicationMVC.Models;
using WebApplicationMVC.Dominio;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Usuarios()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> ListarUsuariosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = await _context.Usuarios.ToListAsync();

            return Json(new { data = usuarios});
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Crear([FromBody] Usuario objeto)
        {
            bool resultado = false;

            try
            {
                if (ModelState.IsValid)
                {
                    string clave = claveHandler.generarClave();

                    string asunto = "Creación de Cuenta";
                    string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                    mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);

                    bool respuesta = claveHandler.EnviarCorreo(objeto.Correo, asunto, mensajeCorreo);

                    if (respuesta)
                    {
                        objeto.Clave = claveHandler.convertirSha256(clave);

                        _context.Usuarios.Add(objeto);
                        await _context.SaveChangesAsync();

                        resultado = true;
                        return StatusCode(StatusCodes.Status200OK,
                            Json(new { resultado = resultado, usuario = objeto, mensaje = "Usuario creado correctamente." }));

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error al enviar la clave por correo.");
                    }
                    
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        Json(new { resultado = resultado, usuario = objeto, mensaje = ModelState }));
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al crear el usuario.");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Editar([FromBody] Usuario objeto)
        {
            bool resultado = false;

            try
            {
                Usuario usuario = await _context.Usuarios.FindAsync(objeto.IdUsuario);
                if (usuario == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        Json(new { resultado = resultado, mensaje = $"Usuario con Id = {objeto.IdUsuario} no encontrado." }));
                }

                if (ModelState.IsValid)
                {   
                    //de está forma la entidad de usuario deja de ser trackeada y se puede realizar otra consulta referenciando el mismo id.
                    _context.Entry(usuario).State = EntityState.Detached;

                    _context.Usuarios.Update(objeto);
                    await _context.SaveChangesAsync();

                    resultado = true;
                    return StatusCode(StatusCodes.Status200OK,
                        Json(new { resultado = resultado, usuario = objeto, mensaje = "Usuario modificado correctamente." }));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        Json(new { resultado = resultado, usuario = objeto, mensaje = ModelState }));
                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al editar el usuario.");
            }

        }
        [HttpDelete]
        public async Task<ActionResult<Usuario>> EliminarUsuario(int id)
        {
            bool resultado = false;

            try
            {
                Usuario usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {               
                    return StatusCode(StatusCodes.Status404NotFound,
                        Json(new { resultado = resultado, mensaje = $"Usuario con Id = {id} no encontrado." }));
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                resultado = true;
                return StatusCode(StatusCodes.Status200OK, Json(new { resultado = resultado,
                    mensaje = $"Usuario con Id = {id} eliminado correctamente." } ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}