using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class PAISES
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
    }

}
