using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using RealState_WEB.Filtros;
using RealState_WEB.Model;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class PropiedadesController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PropiedadesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Propiedades()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedades";
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadesJson = await respuesta.Content.ReadAsStringAsync();
                var propiedadesList = JsonSerializer.Deserialize<List<PROPIEDADES>>(propiedadesJson);

                // Filtrar las propiedades donde el estado es true
                var propiedadesFiltradas = propiedadesList.Where(p => p.estado == true).ToList();

                return View(propiedadesFiltradas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> ConsultarPropiedades()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedades";
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadesJson = await respuesta.Content.ReadAsStringAsync();
                var propiedadesList = JsonSerializer.Deserialize<List<PROPIEDADES>>(propiedadesJson);
                return View(propiedadesList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [FiltroLogin]
        public async Task<IActionResult> MostrarPropiedad(long id)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedad/" + id;
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadJson = await respuesta.Content.ReadAsStringAsync();
                var propiedad = JsonSerializer.Deserialize<PROPIEDADES>(propiedadJson);

                long idUsuario = 0;
                var idUsuarioSession = HttpContext.Session.GetInt32("_id");
                if (idUsuarioSession != null)
                {
                    idUsuario = (long)idUsuarioSession;
                }

                // Realiza la llamada al método ConsultarCita y espera su resultado
                var consultaCitaUrl = $"https://localhost:7273/api/Citas/ConsultarCita?usr={idUsuario}&prop={id}";
                var consultaCitaRespuesta = await client.GetAsync(consultaCitaUrl);

                if (consultaCitaRespuesta.IsSuccessStatusCode)
                {
                    var citaJson = await consultaCitaRespuesta.Content.ReadAsStringAsync();
                    var cita = JsonSerializer.Deserialize<PROPIEDADES_CITAS>(citaJson);

                    // Asigna la propiedad a la cita antes de pasarla a la vista
                    cita.propiedad = propiedad;

                    return View(cita);
                }
                else
                {
                    // Si no hay cita, crea una instancia de PROPIEDADES_CITAS con la propiedad
                    PROPIEDADES_CITAS citas = new PROPIEDADES_CITAS();
                    citas.propiedad = propiedad;
                    return View(citas);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> NuevaPropiedad()
        {
            try
            {
                PROPIEDADES nPropiedad = new PROPIEDADES();
                nPropiedad.propiedadTipo = new PROPIEDAD_TIPOS();
                nPropiedad.propiedadTipo.tiposList = await PropiedadTipos();
                nPropiedad.direccion = new PROPIEDAD_DIRECCIONES();
                nPropiedad.direccion.pais = new PAISES();
                nPropiedad.direccion.pais.paisesList = await Paises();
                nPropiedad.direccion.provincia = new PROVINCIAS();
                nPropiedad.direccion.provincia.provinciaList = await Provincias();
                return View(nPropiedad);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [FiltroAdmin]
        public async Task<IActionResult> NuevaPropiedad(PROPIEDADES propiedad)
        {
            try
            {
                // Procesar las imágenes y asignar las rutas a la propiedad
                propiedad = await ProcesarImagenes(propiedad);

                // Guardar la propiedad en la base de datos

                return RedirectToAction("ConsultarPropiedades");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        private async Task<PROPIEDADES> ProcesarImagenes(PROPIEDADES propiedad)
        {
            // Crear una lista para almacenar las rutas de las imágenes
            List<string> rutasImagenes = new List<string>();

            // Verificar si la lista de imágenes existentes es nula y crearla si es necesario
            if (propiedad.imagenes == null)
            {
                propiedad.imagenes = new List<PROPIEDAD_IMAGENES>();
            }

            // Borrar imágenes en el servidor según imagenesToDel
            if (propiedad.imagenesToDel != null)
            {
                foreach (var imagenToDel in propiedad.imagenesToDel)
                {
                    if (imagenToDel.imagen != null)
                    {
                        // Obtener la ruta completa del archivo a eliminar
                        string rutaCompleta = _hostingEnvironment.WebRootPath.ToString() + imagenToDel.imagen;

                        // Borrar el archivo en la ruta especificada
                        System.IO.File.Delete(rutaCompleta);

                        // Eliminar la imagen de propiedad.imagenes
                        propiedad.imagenes.RemoveAll(imagen => imagen.imagen == imagenToDel.imagen);
                    }
                }
            }


            // Procesar cada imagen
            if (propiedad.imagenesIForm != null)
            {
                foreach (var imagen in propiedad.imagenesIForm)
                {
                    // Generar un nombre aleatorio único para la imagen
                    string nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);

                    // Ruta donde se guardará la imagen en el servidor
                    string rutaImagen = Path.Combine(_hostingEnvironment.WebRootPath, "img/Propiedades", nombreImagen);

                    // Guardar la imagen en la ruta especificada
                    using (var stream = new FileStream(rutaImagen, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // Agregar la ruta de la imagen a la lista
                    rutasImagenes.Add("/img/Propiedades/" + nombreImagen);
                }
                // Añadir las rutas de las imágenes procesadas a la lista existente de imágenes
                propiedad.imagenes.AddRange(rutasImagenes.Select(ruta => new PROPIEDAD_IMAGENES { imagen = ruta }));

                // Establecer el atributo imagenesIForm en null para evitar problemas al serializar a JSON
                propiedad.imagenesIForm = null;
            }

            return propiedad;
        }



        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> ActualizarPropiedad(long id)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedad/" + id;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadJson = await respuesta.Content.ReadAsStringAsync();
                var propiedad = JsonSerializer.Deserialize<PROPIEDADES>(propiedadJson);

                propiedad.propiedadTipo.tiposList = await PropiedadTipos();
                propiedad.direccion.pais.paisesList = await Paises();
                propiedad.direccion.provincia.provinciaList = await Provincias();
                propiedad.imagenesToDel = propiedad.imagenes;
                return View(propiedad);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [FiltroAdmin]
        public async Task<IActionResult> ActualizarPropiedad(PROPIEDADES propiedadActualizada)
        {
            try
            {
                int countDelete = 0;
                if (propiedadActualizada.imagenesToDel != null)
                {
                    foreach (var imagen in propiedadActualizada.imagenesToDel)
                    {
                        if (imagen.imagen != null)
                        {
                            countDelete++;
                        }
                    }
                }


                if (ModelState.IsValid && (propiedadActualizada.imagenes.Count > countDelete || propiedadActualizada.imagenesIForm != null))
                {
                    // Procesar las imágenes y asignar las rutas a la propiedad
                    propiedadActualizada = await ProcesarImagenes(propiedadActualizada);

                    using var client = new HttpClient();
                    var apiUrl = "https://localhost:7273/api/Propiedades/ActualizarPropiedad";

                    JsonContent body = JsonContent.Create(propiedadActualizada);

                    var response = await client.PostAsync(apiUrl, body);

                    if (response.IsSuccessStatusCode)
                    {
                        // Mostrar Sweet Alert
                        TempData["SweetAlertMessage"] = "La propiedad se actualizó correctamente.";
                        TempData["SweetAlertType"] = "success";

                        return RedirectToAction("ConsultarPropiedades");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    propiedadActualizada.propiedadTipo.tiposList = await PropiedadTipos();
                    propiedadActualizada.direccion.pais.paisesList = await Paises();
                    propiedadActualizada.direccion.provincia.provinciaList = await Provincias();
                    propiedadActualizada.imagenesToDel = null;
                    TempData["SweetAlertMessage"] = "La propiedad debe tener imagenes";
                    TempData["SweetAlertType"] = "warning";
                    return View(propiedadActualizada);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }



        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PROPIEDAD_TIPOS>> PropiedadTipos()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/PropiedadTipos/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var tiposJson = await respuesta.Content.ReadAsStringAsync();
                var tiposList = JsonSerializer.Deserialize<List<PROPIEDAD_TIPOS>>(tiposJson);

                return tiposList;
            }
            else
            {
                return new List<PROPIEDAD_TIPOS>();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PAISES>> Paises()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Paises/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var paisesJson = await respuesta.Content.ReadAsStringAsync();
                var paisesList = JsonSerializer.Deserialize<List<PAISES>>(paisesJson);

                return paisesList;
            }
            else
            {
                return new List<PAISES>();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PROVINCIAS>> Provincias()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Provincias/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var provinciasJson = await respuesta.Content.ReadAsStringAsync();
                var provinciasList = JsonSerializer.Deserialize<List<PROVINCIAS>>(provinciasJson);

                return provinciasList;
            }
            else
            {
                return new List<PROVINCIAS>();
            }
        }

        // Intercambia el estado del usuario,si es true (Activo) lo pasa a false (Inactivo)

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> CambiarEstado(long id)
        {
            try
            {
                using var client = new HttpClient();
                var apiUrl = "https://localhost:7273/api/Propiedades/CambiarEstado/" + id;
                var respuesta = await client.GetAsync(apiUrl);
                if (respuesta.IsSuccessStatusCode)
                {
                    // Mostrar Sweet Alert
                    TempData["SweetAlertMessage"] = "Se ha cambiado el estado correctamente";
                    TempData["SweetAlertType"] = "success";
                    return RedirectToAction("ConsultarPropiedades");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
