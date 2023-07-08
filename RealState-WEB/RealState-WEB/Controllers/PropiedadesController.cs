using Microsoft.AspNetCore.Mvc;
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
                return View(propiedadesList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        [HttpGet]
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
                // OJO Falta guardar en la bitácora
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
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
                // OJO Falta guardar en la bitácora
                return RedirectToAction("Index");
            }
        }


        private async Task<PROPIEDADES> ProcesarImagenes(PROPIEDADES propiedad)
        {
            // Crear una lista para almacenar las rutas de las imágenes
            List<string> rutasImagenes = new List<string>();

            // Procesar cada imagen
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

            // Asignar las rutas de las imágenes a la propiedad
            propiedad.imagenes = rutasImagenes.Select(ruta => new PROPIEDAD_IMAGENES { imagen = ruta }).ToList();

            // Establecer el atributo imagenesIForm en null para evitar problemas al serializar a JSON
            propiedad.imagenesIForm = null;

            return propiedad;
        }


        [HttpGet]
        public async Task<IActionResult> ActualizarPropiedad(long id)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Propiedad/"+id;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var propiedadJson = await respuesta.Content.ReadAsStringAsync();
                var propiedad = JsonSerializer.Deserialize<PROPIEDADES>(propiedadJson);

                propiedad.propiedadTipo.tiposList = await PropiedadTipos();
                propiedad.direccion.pais.paisesList = await Paises();
                propiedad.direccion.provincia.provinciaList = await Provincias();
                return View(propiedad);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPropiedad(PROPIEDADES propiedadActualizada)
        {
            try
            {
                using var client = new HttpClient();
                var apiUrl = "https://localhost:7273/api/Propiedades/ActualizarPropiedad";

                JsonContent body = JsonContent.Create(propiedadActualizada);

                var response = await client.PostAsync(apiUrl, body);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ConsultarPropiedades");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // OJO Falta guardar en la bitácora
                return RedirectToAction("Index");
            }
            
        }


        [HttpGet]
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
    }
}
