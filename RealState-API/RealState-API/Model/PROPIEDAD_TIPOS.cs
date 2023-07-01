using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_TIPOS
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }
}
