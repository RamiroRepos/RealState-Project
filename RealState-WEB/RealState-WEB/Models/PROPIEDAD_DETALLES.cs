using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROPIEDAD_DETALLES
    {
        [Key]
        public long id { get; set; }

        [Required(ErrorMessage = "Favor ingrese la cantidad de baños")]
        [DisplayName("Cantidad de Baños")]
        public long cantidad_bannos { get; set; }

        [Required(ErrorMessage = "Favor ingrese la cantidad de cuartos")]
        [DisplayName("Cantidad de Cuartos")]
        public long cantidad_cuartos { get; set; }

        [Required(ErrorMessage = "Favor ingrese la cantidad de parqueos")]
        [DisplayName("Cantidad de Parqueos")]
        public long cantidad_parqueo { get; set; }

        [Required(ErrorMessage = "Favor ingrese la cantidad de metros cuadrados")]
        [DisplayName("Cantidad de Metros Cuadrados")]
        public long cantidad_metros2 { get; set; }

    }
}
