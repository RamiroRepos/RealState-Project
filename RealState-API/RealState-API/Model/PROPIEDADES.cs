using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDADES
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public long Id_Tipo_Fk { get; set; }
        public long Id_Usuario_Fk { get; set; }
        public long Id_Detalle_Fk { get; set; }
        public long Id_Direccion_Fk { get; set; }

        [ForeignKey("Id_Tipo_Fk")]
        public PROPIEDAD_TIPOS PropiedadTipo { get; set; }
    }
}
