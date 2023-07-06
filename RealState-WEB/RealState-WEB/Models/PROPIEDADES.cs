using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealState_WEB.Model
{
    public class PROPIEDADES
    {
        [Key]
        public long id { get; set; }

        
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Favor ingrese el nombre de la propiedad")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Favor ingrese la descripción de la propiedad")]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Favor ingrese el precio de la propiedad")]
        [DisplayName("Precio")]
        public decimal precio { get; set; }

        [Required(ErrorMessage = "Favor ingrese el estado de la propiedad")]
        [DisplayName("Estado")]
        public bool estado { get; set; }

        public long id_tipo_fk { get; set; }
        public long id_usuario_fk { get; set; }
        public long id_detalle_fk { get; set; }
        public long id_direccion_fk { get; set; }

        [ForeignKey("id_tipo_fk")]
        public PROPIEDAD_TIPOS propiedadTipo { get; set; }

        [ForeignKey("id_usuario_fk")]
        public USUARIOS usuario { get; set; }

        [ForeignKey("id_detalle_fk")]
        public PROPIEDAD_DETALLES detalle { get; set; }

        [ForeignKey("id_direccion_fk")]
        public PROPIEDAD_DIRECCIONES direccion { get; set; }
        public List<PROPIEDAD_IMAGENES> imagenes { get; set; }
        public List<IFormFile> imagenesIForm { get; set; }
    }
}
