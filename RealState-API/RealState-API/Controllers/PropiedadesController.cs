﻿using Microsoft.AspNetCore.Mvc;
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
                .Include(p => p.direccion.pais)
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
                .Include(p => p.direccion.pais)
                .Include(p => p.direccion.provincia)
                .FirstOrDefault(p => p.id == id);


            if (propiedad == null)
            {
                return NotFound();
            }

            // Obtener las imágenes de la propiedad y las asigna a la propiedad Imagenes
            propiedad.imagenes = _context.PROPIEDAD_IMAGENES
                .Where(pi => pi.id_propiedad_fk == propiedad.id).ToList();

            return propiedad;
        }

        //Actualizar una propiedad
        [Route("ActualizarPropiedad")]
        [HttpPost]
        public ActionResult ActualizarPropiedad(PROPIEDADES propiedadNueva)
        {
            var propiedadExistente = _context.PROPIEDADES.Include(p => p.propiedadTipo)
                                                         .Include(p => p.usuario)
                                                         .Include(p => p.detalle)
                                                         .Include(p => p.direccion)
                                                         .Include(p => p.direccion.pais)
                                                         .Include(p => p.direccion.provincia)
                                                         .FirstOrDefault(p => p.id == propiedadNueva.id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

            // Obtener las imágenes de la propiedad y las asigna a la propiedad Imagenes
            propiedadExistente.imagenes = _context.PROPIEDAD_IMAGENES
                .Where(pi => pi.id_propiedad_fk == propiedadNueva.id).ToList();

            // Actualizar los valores de la propiedad existente con los nuevos valores
            propiedadExistente.nombre = propiedadNueva.nombre;
            propiedadExistente.descripcion = propiedadNueva.descripcion;
            propiedadExistente.precio = propiedadNueva.precio;
            propiedadExistente.estado = propiedadNueva.estado;

            // Actualizar el tipo de propiedad
            propiedadExistente.id_tipo_fk = propiedadNueva.id_tipo_fk;

            // Actualizar los valores de la tabla detalle
            propiedadExistente.detalle.cantidad_bannos = propiedadNueva.detalle.cantidad_bannos;
            propiedadExistente.detalle.cantidad_cuartos = propiedadNueva.detalle.cantidad_cuartos;
            propiedadExistente.detalle.cantidad_parqueo = propiedadNueva.detalle.cantidad_parqueo;
            propiedadExistente.detalle.cantidad_metros2 = propiedadNueva.detalle.cantidad_metros2;

            // Actualizar los valores de la tabla direccion
            propiedadExistente.direccion.direccion_exacta = propiedadNueva.direccion.direccion_exacta;
            propiedadExistente.direccion.canton = propiedadNueva.direccion.canton;
            propiedadExistente.direccion.distrito = propiedadNueva.direccion.distrito;
            propiedadExistente.direccion.id_pais_fk = propiedadNueva.direccion.pais.id;
            propiedadExistente.direccion.id_provincia_fk = propiedadNueva.direccion.provincia.id;

            // Se actualizan las imágenes
            int cantImgExistentes = propiedadExistente.imagenes.Count;
            int cantImgNuevas = propiedadNueva.imagenes.Count;

            // Actualizar, eliminar o agregar imágenes
            for (int i = 0; i < Math.Max(cantImgNuevas, cantImgExistentes); i++)
            {
                if (i < cantImgExistentes)
                {
                    PROPIEDAD_IMAGENES imagenExistente = propiedadExistente.imagenes[i];
                    if (i < cantImgNuevas)
                    {
                        // Actualizar las imágenes existentes
                        imagenExistente.imagen = propiedadNueva.imagenes[i].imagen;
                    }
                    else
                    {
                        // Eliminar imágenes existentes que sobran
                        _context.PROPIEDAD_IMAGENES.Remove(imagenExistente);
                    }
                }
                else
                {
                    // Agregar nuevas imágenes
                    var nuevaImagen = new PROPIEDAD_IMAGENES
                    {
                        imagen = propiedadNueva.imagenes[i].imagen,
                        id_propiedad_fk = propiedadNueva.id // Asigna el ID de la propiedad correspondiente
                    };
                    // Agregar la nueva imagen al contexto
                    _context.PROPIEDAD_IMAGENES.Add(nuevaImagen);
                }
            }
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
        public ActionResult<PROPIEDADES> CreatePropiedad(PROPIEDADES propiedad)
        {
            try
            {
                propiedad.propiedadTipo = null;
                propiedad.direccion.pais = null;
                propiedad.direccion.provincia= null;

                _context.PROPIEDADES.Add(propiedad);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                return Ok(propiedad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la propiedadNueva: {ex.Message}");
            }
        }

        ////////////////////////////// Acciones de Tipos de Propiedades //////////////////////////////

        [Route("PropiedadTipos")]
        [HttpGet]
        public ActionResult<List<PROPIEDAD_TIPOS>> PropiedadTipos()
        {
            return _context.PROPIEDAD_TIPOS.ToList();
        }

        ////////////////////////////// Acciones de Paises //////////////////////////////

        [Route("Paises")]
        [HttpGet]
        public ActionResult<List<PAISES>> Paises()
        {
            return _context.PAISES.ToList();
        }

        ////////////////////////////// Acciones de Provincias //////////////////////////////

        [Route("Provincias")]
        [HttpGet]
        public ActionResult<List<PROVINCIAS>> Provincias()
        {
            return _context.PROVINCIAS.ToList();
        }

        /// HMR
        // Actualiza el estado del usuario
        [Route("CambiarEstado/{id}")]
        [HttpGet]
        public ActionResult<PROPIEDADES> CambiarEstado(long id)
        {
            // Buscar la propiedad existente en la base de datos
            var propiedadExistente = _context.PROPIEDADES.FirstOrDefault(u => u.id == id);

            if (propiedadExistente == null)
            {
                return NotFound();
            }

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

            return Ok();
        }

        ////////////////////////////// Acciones de Provincias //////////////////////////////

        [Route("AgendarCita")]
        [HttpGet]
        public ActionResult<PROPIEDADES_CITAS> AgendarCita([FromQuery] long prop, [FromQuery] long usr, [FromQuery] DateTime dt)
        {
            try
            {
                PROPIEDADES_CITAS cita = new PROPIEDADES_CITAS();
                cita.id_propiedad = prop;
                cita.id_usuario = usr;
                cita.fecha_hora = dt;
                _context.PROPIEDADES_CITAS.Add(cita);

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                return Ok(cita);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agendar la ciat: {ex.Message}");
            }
        }
    }
}
