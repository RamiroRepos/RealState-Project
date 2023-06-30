using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDADES_CITAS
    {
        [Key]
        public long ID { get; set; }
        public DateTime FECHA_HORA { get; set; }
        public long ID_PROPIEDAD { get; set; }
        public long ID_USUARIO { get; set; }

        [ForeignKey("ID_PROPIEDAD")]
        public PROPIEDADES Propiedad { get; set; }

        [ForeignKey("ID_USUARIO")]
        public USUARIOS Usuario { get; set; }
    }

}
