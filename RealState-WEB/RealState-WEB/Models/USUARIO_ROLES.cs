using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class USUARIO_ROLES
    {
        [Key]
        public long id { get; set; }

        [Required(ErrorMessage = "Favor ingrese el nombre")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Favor ingrese la descripción")]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

    }

}
