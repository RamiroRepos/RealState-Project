using Microsoft.AspNetCore.Mvc;
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
    }
}
