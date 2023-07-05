using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealState_WEB.Model;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class PropiedadesController : Controller
    {
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
