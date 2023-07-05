using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROVINCIAS
    {
        [Key]
        public long id { get; set; }
        [DisplayName("Provincia")]
        public string nombre { get; set; }
        public List<PROVINCIAS>? provinciaList { get; set; }
        public List<SelectListItem>? ProvinciaListSelectList
        {
            get
            {
                if (provinciaList != null)
                {
                    return provinciaList.Select(t => new SelectListItem
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
