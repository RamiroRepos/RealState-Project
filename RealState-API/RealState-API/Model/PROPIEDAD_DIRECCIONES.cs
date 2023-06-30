using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_DIRECCIONES
    {
        [Key]
        public long ID { get; set; }
        public string DIRECCION_EXACTA { get; set; }
        public string GMAPS_LINK { get; set; }
        public long ID_PAIS_FK { get; set; }
        public long ID_PROVINCIA_FK { get; set; }
        public string CANTON { get; set; }
        public string DISTRITO { get; set; }

        [ForeignKey("ID_PAIS_FK")]
        public PAISES PAIS { get; set; }

        [ForeignKey("ID_PROVINCIA_FK")]
        public PROVINCIAS PROVINCIA { get; set; }
    }

}
