using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Filtros;
using RealState_WEB.Model;
using System.Globalization;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class CitasController : Controller
    {
        [HttpPost]
        [FiltroLogin]
        public async Task<IActionResult> AgendarCita(PROPIEDADES_CITAS entidad)
        {
            try
            {
                if (entidad.fecha_hora != null && entidad.hora != null)
                {
                    using var client = new HttpClient();
                    long idPropiedad = entidad.id_propiedad;
                    long idUsuario = (long)HttpContext.Session.GetInt32("_id");
                    entidad.fecha_hora = entidad.fecha_hora.Add(TimeSpan.ParseExact(entidad.hora, "hh\\:mm", CultureInfo.InvariantCulture));

                    DateTime fechaHora = entidad.fecha_hora;

                    var apiUrl = $"https://localhost:7273/api/Propiedades/AgendarCita?prop={idPropiedad}&usr={idUsuario}&dt={fechaHora}";

                    var respuesta = await client.GetAsync(apiUrl);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["SweetAlertMessage"] = "Cita agendada con éxito";
                        TempData["SweetAlertType"] = "success";
                        return RedirectToAction("MostrarPropiedad", "Propiedades", new
                        {
                            id = entidad.id_propiedad,
                        });
                    }
                    else
                    {
                        TempData["SweetAlertMessage"] = "Error al agendar cita";
                        TempData["SweetAlertType"] = "error";
                        return RedirectToAction("MostrarPropiedad", "Propiedades", new
                        {
                            id = entidad.id_propiedad,

                        });
                    }
                }
                else
                {
                    TempData["SweetAlertMessage"] = "Debe seleccionar la fecha y la hora de la cita";
                    TempData["SweetAlertType"] = "warning";
                    return RedirectToAction("MostrarPropiedad", "Propiedades", new
                    {
                        id = entidad.id_propiedad,

                    });
                }
            }
            catch (Exception ex)
            {
                TempData["SweetAlertMessage"] = "Error al agendar cita";
                TempData["SweetAlertType"] = "error";
                return RedirectToAction("MostrarPropiedad", "Propiedades", new
                {
                    id = entidad.id_propiedad,

                });
            }
        }

        [HttpGet]
        [FiltroLogin]
        public async Task<IActionResult> MisCitas()
        {
            using var client = new HttpClient();
            var idUsuario = HttpContext.Session.GetInt32("_id");
            var apiUrl = "https://localhost:7273/api/Citas/MisCitas/" + idUsuario;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var CitasJson = await respuesta.Content.ReadAsStringAsync();
                var CitasList = JsonSerializer.Deserialize<List<PROPIEDADES_CITAS>>(CitasJson);
                return View(CitasList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [FiltroLogin]
        public async Task<IActionResult> CancelarCita(long id, long idPropiedad, string vistaOrigen)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Citas/CancelarCita/" + id;
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                if (vistaOrigen == "MisCitas")
                {
                    return RedirectToAction("MisCitas");
                }
                else if (vistaOrigen == "MostrarPropiedad")
                {
                    return RedirectToAction("MostrarPropiedad", "Propiedades", new { id = idPropiedad });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}