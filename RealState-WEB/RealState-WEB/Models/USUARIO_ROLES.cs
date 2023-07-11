using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class USUARIO_ROLES
    {
        [Key]
        [Required(ErrorMessage = "*Favor seleccione el rol")]
        public long? id { get; set; }

        [Required(ErrorMessage = "*Favor ingrese el nombre")]
        [DisplayName("Rol")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "*Favor ingrese la descripción")]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        public List<USUARIO_ROLES>? rolesList { get; set; }
        public List<SelectListItem>? RolesListSelectList
        {
            get
            {
                if (rolesList != null)
                {
                    return rolesList.Select(t => new SelectListItem
                    {
                        Value = t.id.ToString(),
                        Text = t.nombre + " - " + t.descripcion
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
