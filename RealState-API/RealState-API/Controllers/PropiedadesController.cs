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

        //// Obtener todas las propiedades
        [Route("Propiedades")]
        [HttpGet]
        public ActionResult<List<PROPIEDADES>> Propiedades()
        {
            var propiedades = _context.PROPIEDADES
                .Include(p => p.propiedadTipo)
                .Include(p => p.usuario)
                .Include(p => p.usuario.rol)
                .Include(p => p.usuario.direccion)
                .Include(p => p.detalle)
                .Include(p => p.direccion)
                .Include(p => p.direccion.PAIS)
                .Include(p => p.direccion.provincia)
                .ToList();

            // Obtener las imágenes para cada propiedad y asignarlas a la propiedad Imagenes
            foreach (var propiedad in propiedades)
            {
                propiedad.imagenes = _context.PROPIEDAD_IMAGENES
                    .Where(pi => pi.id_propiedad_fk == propiedad.id).ToList();
            }

            return propiedades;
        }

        [Route("Imagenes")]
        [HttpGet]
        public ActionResult<List<PROPIEDAD_IMAGENES>> ObtenerImagenes(long idPropiedad)
        {
            var imagenes = _context.PROPIEDAD_IMAGENES
                .Where(i => i.id_propiedad_fk == idPropiedad).ToList();
            return imagenes;
        }

        // Obtener un propiedad
        [Route("Propiedad/{id}")]
        [HttpGet]
        public ActionResult<PROPIEDADES> Propiedad(long id)
        {
            var propiedad = _context.PROPIEDADES
                .Include(p => p.propiedadTipo)
                .Include(p => p.usuario)
                .Include(p => p.usuario.rol)
                .Include(p => p.usuario.direccion)
                .Include(p => p.detalle)
                .Include(p => p.direccion)
                .Include(p => p.direccion.PAIS)
                .Include(p => p.direccion.provincia)
                .FirstOrDefault(p => p.id == id);

            if (propiedad == null)
            {
                return NotFound();
            }

            return propiedad;
        }

        //Actualizar una propiedad
        [Route("ActualizarPropiedad")]
        [HttpPut]
        public ActionResult ActualizarPropiedad(long id, PROPIEDADES propiedad) // OJO hay que ver si se una el mismo ID que traiga el objeto propiedad y quitar ese "long id"
        {
            var propiedadExistente = _context.PROPIEDADES.Include(p => p.propiedadTipo)
                                                         .Include(p => p.usuario)
                                                         .Include(p => p.detalle)
                                                         .Include(p => p.direccion)
                                                         .FirstOrDefault(p => p.id == id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

            // Actualizar los valores de la propiedad existente con los nuevos valores
            propiedadExistente.nombre = propiedad.nombre;
            propiedadExistente.descripcion = propiedad.descripcion;
            propiedadExistente.precio = propiedad.precio;
            propiedadExistente.estado = propiedad.estado;

            // Actualizar los valores de la tabla detalle
            propiedadExistente.detalle.cantidad_bannos = propiedad.detalle.cantidad_bannos;
            propiedadExistente.detalle.cantidad_cuartos = propiedad.detalle.cantidad_cuartos;
            propiedadExistente.detalle.cantidad_parqueo = propiedad.detalle.cantidad_parqueo;
            propiedadExistente.detalle.cantidad_metros2 = propiedad.detalle.cantidad_metros2;

            // Actualizar los valores de la tabla direccion
            propiedadExistente.direccion.direccion_exacta = propiedad.direccion.direccion_exacta;
            propiedadExistente.direccion.gmaps_link = propiedad.direccion.gmaps_link;
            propiedadExistente.direccion.canton = propiedad.direccion.canton;
            propiedadExistente.direccion.distrito = propiedad.direccion.distrito;

            //OJO falta la parte de imagenes

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(propiedadExistente);
        }

        //Actualizar el estado de alguna propiedad
        [Route("ActualizarPropiedadEstado")]
        [HttpPut]
        public ActionResult ActualizarPropiedadEstado(long id)
        {
            var propiedadExistente = _context.PROPIEDADES.FirstOrDefault(p => p.id == id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

            // Actualizar el estado de la propiedad
            if (propiedadExistente.estado)
            {
                propiedadExistente.estado = false;
            }
            else
            {
                propiedadExistente.estado = true;
            }
            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(propiedadExistente);
        }

        [Route("NuevaPropiedad")]
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

        ////////////////////////////// Acciones de Tipos de Propiedades //////////////////////////////

        [Route("PropiedadTipos")]
        [HttpGet]
        public ActionResult<List<PROPIEDAD_TIPOS>> GetPropiedadTipos()
        {
            return _context.PROPIEDAD_TIPOS.ToList();
        }
    }
}
