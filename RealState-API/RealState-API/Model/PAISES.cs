using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PAISES
    {
        [Key]
        public long ID { get; set; }
        public string NOMBRE { get; set; }
    }

}
