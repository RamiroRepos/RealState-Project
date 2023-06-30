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

        ////////////////////////////// Acciones de Propiedades //////////////////////////////

        // Obtener todas las propiedades
        [Route("GetPropiedades")]
        [HttpGet]
        public ActionResult<List<PROPIEDADES>> GetPropiedades()
        {
            return _context.PROPIEDADES
                .Include(p => p.PropiedadTipo)
                .Include(p => p.Usuario)
                .Include(p => p.Detalle)
                .Include(p => p.Direccion)
                .ToList();
        }

        // Obtener un propiedad
        [Route("GetPropiedad/{id}")]
        [HttpGet]
        public ActionResult<PROPIEDADES> GetPropiedad(long id)
        {
            var propiedad = _context.PROPIEDADES
                .Include(p => p.PropiedadTipo)
                .Include(p => p.Usuario)
                .Include(p => p.Detalle)
                .Include(p => p.Direccion)
                .FirstOrDefault(p => p.ID == id);

            if (propiedad == null)
            {
                return NotFound();
            }

            return propiedad;
        }

        //Actualizar una propiedad
        [Route("UpdatePropiedad")]
        [HttpPut]
        public ActionResult UpdatePropiedad(long id, PROPIEDADES propiedad) // OJO hay que ver si se una el mismo ID que traiga el objeto propiedad y quitar ese "long id"
        {
            var propiedadExistente = _context.PROPIEDADES.Include(p => p.PropiedadTipo)
                                                         .Include(p => p.Usuario)
                                                         .Include(p => p.Detalle)
                                                         .Include(p => p.Direccion)
                                                         .FirstOrDefault(p => p.ID == id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

            // Actualizar los valores de la propiedad existente con los nuevos valores
            propiedadExistente.NOMBRE = propiedad.NOMBRE;
            propiedadExistente.DESCRIPCION = propiedad.DESCRIPCION;
            propiedadExistente.PRECIO = propiedad.PRECIO;
            propiedadExistente.ESTADO = propiedad.ESTADO;

            // Actualizar los valores de la tabla Detalle
            propiedadExistente.Detalle.CANTIDAD_BANNOS = propiedad.Detalle.CANTIDAD_BANNOS;
            propiedadExistente.Detalle.CANTIDAD_CUARTOS = propiedad.Detalle.CANTIDAD_CUARTOS;
            propiedadExistente.Detalle.CANTIDAD_PARQUEO = propiedad.Detalle.CANTIDAD_PARQUEO;
            propiedadExistente.Detalle.CANTIDAD_METROS2 = propiedad.Detalle.CANTIDAD_METROS2;

            // Actualizar los valores de la tabla Direccion
            propiedadExistente.Direccion.DIRECCION_EXACTA = propiedad.Direccion.DIRECCION_EXACTA;
            propiedadExistente.Direccion.GMAPS_LINK = propiedad.Direccion.GMAPS_LINK;
            propiedadExistente.Direccion.CANTON = propiedad.Direccion.CANTON;
            propiedadExistente.Direccion.DISTRITO = propiedad.Direccion.DISTRITO;

            //OJO falta la parte de imagenes

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(propiedadExistente);
        }

        //Actualizar el estado de alguna propiedad
        [Route("UpdatePropiedadEstado")]
        [HttpPut]
        public ActionResult UpdatePropiedadEstado(long id)
        {
            var propiedadExistente = _context.PROPIEDADES.FirstOrDefault(p => p.ID == id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

            // Actualizar el estado de la propiedad
            if (propiedadExistente.ESTADO)
            {
                propiedadExistente.ESTADO = false;
            }
            else
            {
                propiedadExistente.ESTADO = true;
            }
            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(propiedadExistente);
        }

        [Route("CreatePropiedad")]
        [HttpPost]
        public ActionResult<PROPIEDADES> CreatePropiedad(PROPIEDADES propiedad) //OJO falta lo de imagenes
        {
            try
            {
                // Agregar la nueva propiedad al contexto
                _context.PROPIEDADES.Add(propiedad);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                return Ok(propiedad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la propiedad: {ex.Message}");
            }
        }

        //Acciones de Tipos de Propiedades

        [Route("GetPropiedadTipos")]
        [HttpGet]
        public ActionResult<List<PROPIEDAD_TIPOS>> GetPropiedadTipos()
        {
            return _context.PROPIEDAD_TIPOS.ToList();
        }
    }
}
