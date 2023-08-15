using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealState_API.Model;

namespace RealState_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : Controller
    {
        private readonly REALSTATEContext _context;

        public CitasController(REALSTATEContext context)
        {
            _context = context;
        }

        [Route("MisCitas/{id}")]
        [HttpGet]
        public ActionResult<List<PROPIEDADES_CITAS>> MisCitas(long id)
        {
            var citas = _context.PROPIEDADES_CITAS
                .Include(p => p.propiedad)
                .Include(p => p.usuario)
                .Where(c => c.id_usuario == id)
                .ToList();

            return citas;
        }

        [Route("CancelarCita/{id}")]
        [HttpGet]
        public ActionResult CancelarCita(long id)
        {
            var cita = _context.PROPIEDADES_CITAS.Find(id);

            if (cita == null)
            {
                // Manejo de la cita no encontrada
                return NotFound();
            }
            _context.PROPIEDADES_CITAS.Remove(cita);
            _context.SaveChanges();
            return RedirectToAction("MisCitas", new { id = cita.id_usuario });
        }

        [Route("ConsultarCita")]
        [HttpGet]
        public ActionResult<PROPIEDADES_CITAS> ConsultarCita([FromQuery] long usr, [FromQuery] long prop)
        {
            var validadCita = _context.PROPIEDADES_CITAS.FirstOrDefault(p => p.id_usuario == usr && p.id_propiedad == prop);

            if (validadCita == null)
            {
                return NotFound();
            }

            // Si el usuario y la contraseña son válidos, puedes retornar el objeto USUARIOS
            return validadCita;
        }
    }
}
