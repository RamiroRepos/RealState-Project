using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Filtros;
using RealState_WEB.Model;
using System.Globalization;
using System.Net;

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
    }
}