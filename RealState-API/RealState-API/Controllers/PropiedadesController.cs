using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealState_API.Model;

namespace RealState_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadesController : ControllerBase
    {
        private readonly REALSTATEContext _context;

        public PropiedadesController(REALSTATEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<PROPIEDADES>> GetPropiedades()
        {
            return _context.PROPIEDADES.Include(p => p.PropiedadTipo).ToList();
        }
         /*
        [HttpGet]
        public ActionResult<List<USUARIOS>> GetUsuarios()
        {
            return _context.USUARIOS.Include(p => p.Direccion).ToList();
        }*/
    }

}
