using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealState_API.Model
{
    public class PROPIEDADES_CITAS
    {
        [Key]
        public long id { get; set; }
        public DateTime fecha_hora { get; set; }
        public long id_propiedad { get; set; }
        public long id_usuario { get; set; }

        [ForeignKey("id_propiedad")]
        [JsonIgnore]
        public PROPIEDADES? propiedad { get; set; }

        [ForeignKey("id_usuario")]
        public USUARIOS usuario { get; set; }
    }

}
