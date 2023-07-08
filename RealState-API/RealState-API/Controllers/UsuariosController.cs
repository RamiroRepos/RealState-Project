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

        // Obtener un propiedad
        [Route("Usuario/{id}")]
        [HttpGet]
        public ActionResult<USUARIOS> Usuario(long id)
        {
            var usuario = _context.USUARIOS
                .Include(u => u.rol)
                .Include(u => u.direccion)
                .FirstOrDefault(p => p.id == id);


            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

    }
}
