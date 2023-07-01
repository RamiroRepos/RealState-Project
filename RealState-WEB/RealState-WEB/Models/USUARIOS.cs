using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_WEB.Model
{
    public class USUARIOS
    {
        [Key]
        public long id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string contrasenna { get; set; }
        public bool estado { get; set; }
        public long id_rol_fk { get; set; }
        public long id_direccion_fk { get; set; }

        [ForeignKey("id_rol_fk")]
        public USUARIO_ROLES rol { get; set; }

        [ForeignKey("id_direccion_fk")]
        public USUARIO_DIRECCIONES direccion { get; set; }
    }

}
