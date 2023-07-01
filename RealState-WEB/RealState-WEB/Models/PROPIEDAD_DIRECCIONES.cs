using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROPIEDAD_DIRECCIONES
    {
        [Key]
        public long id { get; set; }
        public string direccion_exacta { get; set; }
        public string gmaps_link { get; set; }
        public long id_pais_fk { get; set; }
        public long id_provincia_fk { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }

        [ForeignKey("id_pais_fk")]
        public PAISES pais { get; set; }

        [ForeignKey("id_provincia_fk")]
        public PROVINCIAS provincia { get; set; }
    }

}
