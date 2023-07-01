using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PROPIEDAD_DETALLES
    {
        [Key]
        public long id { get; set; }
        public long cantidad_bannos { get; set; }
        public long cantidad_cuartos { get; set; }
        public long cantidad_parqueo { get; set; }
        public long cantidad_metros2 { get; set; }
    }
}
