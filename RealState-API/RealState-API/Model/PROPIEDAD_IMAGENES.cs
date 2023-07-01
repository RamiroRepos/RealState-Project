using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealState_API.Model
{
    public class PROPIEDAD_IMAGENES
    {
        [Key]
        public long id { get; set; }
        public string imagen { get; set; }
        public long id_propiedad_fk { get; set; }
        [JsonIgnore]
        [ForeignKey("id_propiedad_fk")]
        public PROPIEDADES? propiedad { get; set; }
    }

}
