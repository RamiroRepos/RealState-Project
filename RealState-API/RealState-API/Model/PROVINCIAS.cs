using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROVINCIAS
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
    }
}
