using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDADES
    {
        [Key]
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO { get; set; }
        public bool ESTADO { get; set; }
        public long ID_TIPO_FK { get; set; }
        public long ID_USUARIO_FK { get; set; }
        public long ID_DETALLE_FK { get; set; }
        public long ID_DIRECCION_FK { get; set; }

        [ForeignKey("ID_TIPO_FK")]
        public PROPIEDAD_TIPOS PropiedadTipo { get; set; }

        [ForeignKey("ID_USUARIO_FK")]
        public USUARIOS Usuario { get; set; }

        [ForeignKey("ID_DETALLE_FK")]
        public PROPIEDAD_DETALLES Detalle { get; set; }

        [ForeignKey("ID_DIRECCION_FK")]
        public PROPIEDAD_DIRECCIONES Direccion { get; set; }
    }
}
