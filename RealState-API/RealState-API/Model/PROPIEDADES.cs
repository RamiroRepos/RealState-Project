using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDADES
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
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

    }
}
