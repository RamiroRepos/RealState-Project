using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PAISES
    {
        [Key]
        [Required(ErrorMessage = "*Favor seleccione el país")]
        public long? id { get; set; }
        [DisplayName("País")]
        public string? nombre { get; set; }
        public List<PAISES>? paisesList { get; set; }
        public List<SelectListItem>? PaisesListSelectList
        {
            get
            {
                if (paisesList != null)
                {
                    return paisesList.Select(t => new SelectListItem
                    {
                        Value = t.id.ToString(),
                        Text = t.nombre
                    }).ToList();
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }
        }

    }

}
