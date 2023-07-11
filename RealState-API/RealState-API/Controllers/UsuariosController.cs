using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealState_API.Model;

namespace RealState_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {

        private readonly REALSTATEContext _context;

        public UsuariosController(REALSTATEContext context)
        {
            _context = context;
        }

        ////////////////////////////// Acciones de Usuarios //////////////////////////////

        // Obtener todos los usuarios
        [Route("Usuarios")]
        [HttpGet]
        public ActionResult<List<USUARIOS>> Usuarios()
        {
            var usuarios = _context.USUARIOS
                .Include(u => u.rol)
                .Include(u => u.direccion)
                .ToList();

            return usuarios;
        }

        // Obtiene 1 usuario
        [Route("Usuario/{id}")]
        [HttpGet]
        public ActionResult<USUARIOS> Usuario(long id)
        {
            var usuario = _context.USUARIOS
                .Include(u => u.rol)
                .Include(u => u.direccion)
                .Include(p => p.direccion.pais)
                .Include(p => p.direccion.provincia)
                .FirstOrDefault(p => p.id == id);


            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // Crea un usuario
        [Route("Usuario")]
        [HttpPost]
        public ActionResult<USUARIOS> Usuario(USUARIOS usuarioNuevo)
        {
            //var usuario = _context.USUARIOS
            //    .Include(u => u.rol)
            //    .Include(u => u.direccion)
            //    .Include(p => p.direccion.pais)
            //    .Include(p => p.direccion.provincia)
            //    .FirstOrDefault(p => p.id == id);


            //if (usuario == null)
            //{
            //    return NotFound();
            //}

            return null;
        }

        // Actualiza un usuario
        [Route("ActualizarUsuario")]
        [HttpPost]
        public IActionResult ActualizarUsuario(USUARIOS usuario)
        {
            try
            {
                // Buscar el usuario existente en la base de datos
                var usuarioExistente = _context.USUARIOS
                    .Include(u => u.rol)
                    .Include(u => u.direccion)
                    .Include(p => p.direccion.pais)
                    .Include(p => p.direccion.provincia)
                    .FirstOrDefault(u => u.id == usuario.id);

                if (usuarioExistente != null)
                {
                    // Actualizar los valores del usuario existente con los valores del usuario actualizado
                    // Datos Usuario
                    usuarioExistente.nombre = usuario.nombre;
                    usuarioExistente.apellidos = usuario.apellidos;
                    usuarioExistente.email = usuario.email;
                    usuarioExistente.telefono = usuario.telefono;
                    usuarioExistente.contrasenna = usuario.contrasenna;
                    usuarioExistente.estado = usuario.estado;
                    // Rol
                    usuarioExistente.id_rol_fk = usuario.rol.id;
                    // Direccion
                    usuarioExistente.direccion.direccion_exacta = usuario.direccion.direccion_exacta;
                    usuarioExistente.direccion.id_pais_fk = usuario.direccion.pais.id;
                    usuarioExistente.direccion.id_provincia_fk = usuario.direccion.provincia.id;
                    usuarioExistente.direccion.canton = usuario.direccion.canton;
                    usuarioExistente.direccion.distrito = usuario.direccion.distrito;

                    // Guardar los cambios en la base de datos
                    _context.SaveChanges();

                    // Retorna el success
                    return Ok();
                }
                else
                {
                    // El usuario no fue encontrado en la base de datos
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // OJO
                return BadRequest("Error al actualizar el usuario: " + ex.Message);
            }
        }

        // Actualiza el estado del usuario
        [Route("CambiarEstado/{id}")]
        [HttpGet]
        public ActionResult<USUARIOS> CambiarEstado(long id)
        {
            // Buscar el usuario existente en la base de datos
            var usuarioExistente = _context.USUARIOS.FirstOrDefault(u => u.id == id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            if (usuarioExistente.estado)
            {
                usuarioExistente.estado = false;
            }
            else
            {
                usuarioExistente.estado = true;
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok();
        }

        ////////////////////////////// Acciones de Roles de Usuario //////////////////////////////

        // Obtener todos los roles
        [Route("Roles")]
        [HttpGet]
        public ActionResult<List<USUARIO_ROLES>> Roles()
        {
            return _context.USUARIO_ROLES.ToList();
        }
    }
}
