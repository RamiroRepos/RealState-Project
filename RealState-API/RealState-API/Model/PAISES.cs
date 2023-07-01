using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PAISES
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
    }

}
