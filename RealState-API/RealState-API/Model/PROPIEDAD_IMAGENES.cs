using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_IMAGENES
    {
        [Key]
        public long ID { get; set; }
        public string IMAGEN { get; set; }
        public long ID_PROPIEDAD_FK { get; set; }

        [ForeignKey("ID_PROPIEDAD_FK")]
        public PROPIEDADES Propiedad { get; set; }
    }

}
