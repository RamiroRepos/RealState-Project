using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_DETALLES
    {
        [Key]
        public long ID { get; set; }
        public long CANTIDAD_BANNOS { get; set; }
        public long CANTIDAD_CUARTOS { get; set; }
        public long CANTIDAD_PARQUEO { get; set; }
        public long CANTIDAD_METROS2 { get; set; }
    }
}
