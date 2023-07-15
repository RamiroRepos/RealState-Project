using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RealState_WEB.Model
{
    public class PROPIEDAD_DIRECCIONES
    {
        [Key]
        public long id { get; set; }

        [Required(ErrorMessage = "*Favor ingrese la dirección exacta")]
        [DisplayName("Dirección Exacta")]
        public string direccion_exacta { get; set; }
        public long id_pais_fk { get; set; }
        public long id_provincia_fk { get; set; }
        [Required(ErrorMessage = "*Favor ingrese el Cantón")]
        [DisplayName("Cantón")]
        public string canton { get; set; }
        [Required(ErrorMessage = "*Favor ingrese el Distrito")]
        [DisplayName("Distrito")]
        public string distrito { get; set; }

        [ForeignKey("id_pais_fk")]
        public PAISES pais { get; set; }

        [ForeignKey("id_provincia_fk")]
        public PROVINCIAS provincia { get; set; }
    }

}
