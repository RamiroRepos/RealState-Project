using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROPIEDAD_TIPOS
    {
        [Key]
        public long id { get; set; }

        [DisplayName("Tipo Propiedad")]
        public string nombre { get; set; }

        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        public List<PROPIEDAD_TIPOS>? tiposList { get; set; }
        public List<SelectListItem> TiposListSelectList
        {
            get
            {
                if (tiposList != null)
                {
                    return tiposList.Select(t => new SelectListItem
                    {
                        Value = t.id.ToString(),
                        Text = t.nombre
                    }).ToList();
                }
                else
                {
                    return new List<SelectListItem>(); // Devuelve una lista vacía
                }
            }
        }

    }
}
