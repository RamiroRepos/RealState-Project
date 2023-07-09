using RealState_WEB.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealState_WEB.Model
{
    public class PROPIEDADES
    {
        [Key]
        public long id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Favor ingrese el nombre de la propiedad")]
        public string? nombre { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Favor ingrese la descripción de la propiedad")]
        public string? descripcion { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "Favor ingrese el precio de la propiedad")]
        public decimal precio { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "Favor ingrese el estado de la propiedad")]
        public bool estado { get; set; }

        [ForeignKey("id_tipo_fk")]
        public long? id_tipo_fk { get; set; }

        public long? id_usuario_fk { get; set; }
        public long? id_detalle_fk { get; set; }
        public long? id_direccion_fk { get; set; }

        public PROPIEDAD_TIPOS? propiedadTipo { get; set; }

        [ForeignKey("id_usuario_fk")]
        public USUARIOS? usuario { get; set; }

        [ForeignKey("id_detalle_fk")]
        public PROPIEDAD_DETALLES? detalle { get; set; }

        [ForeignKey("id_direccion_fk")]
        public PROPIEDAD_DIRECCIONES? direccion { get; set; }

        public List<PROPIEDAD_IMAGENES>? imagenes { get; set; }

        [JsonIgnore]
        public List<PROPIEDAD_IMAGENES>? imagenesToDel { get; set; }

        [JsonIgnore]
        public List<IFormFile>? imagenesIForm { get; set; }
    }
}
