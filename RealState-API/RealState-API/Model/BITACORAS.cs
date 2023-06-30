using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class BITACORAS
    {
        [Key]
        public long ID { get; set; }
        public DateTime FECHA_HORA { get; set; }
        public string ORIGEN { get; set; }
        public string MENSAJE_ERROR { get; set; }
        public long? ID_USUARIO_FK { get; set; }

        [ForeignKey("ID_USUARIO_FK")]
        public USUARIOS Usuario { get; set; }
    }

}
