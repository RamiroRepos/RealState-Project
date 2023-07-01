using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class USUARIO_ROLES
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }

}
