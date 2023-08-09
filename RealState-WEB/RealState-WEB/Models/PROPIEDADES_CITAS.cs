using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROPIEDADES_CITAS
    {
        [Key]
        public long id { get; set; }
        public DateTime fecha_hora { get; set; } /*= DateTime.Now.AddDays(3);*/
        public string hora { get; set; }
        public long id_propiedad { get; set; }
        public long id_usuario { get; set; }

        [ForeignKey("id_propiedad")]
        public PROPIEDADES propiedad { get; set; }

        [ForeignKey("id_usuario")]
        public USUARIOS usuario { get; set; }
    }

}
