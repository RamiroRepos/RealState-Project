using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_TIPOS
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
