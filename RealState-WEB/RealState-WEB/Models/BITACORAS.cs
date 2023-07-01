using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class BITACORAS
    {
        [Key]
        public long id { get; set; }
        public DateTime fecha_hora { get; set; }
        public string origen { get; set; }
        public string mensaje_error { get; set; }
        public long? id_usuario_fk { get; set; }

        [ForeignKey("id_usuario_fk")]
        public USUARIOS usuario { get; set; }
    }

}
