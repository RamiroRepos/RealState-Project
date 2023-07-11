using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RealState_WEB.Model
{
    public class USUARIOS
    {
        [Key]
        public long id { get; set; }

        [Required(ErrorMessage = "*Favor ingrese el nombre")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "*Favor ingrese los apellidos")]
        [DisplayName("Apellidos")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "*Favor ingrese el email")]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "*Favor ingrese el teléfono")]
        [DisplayName("Teléfono")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "*Favor ingrese la contraseña")]
        [DisplayName("Contraseña")]
        public string contrasenna { get; set; }
        [Required(ErrorMessage = "*Favor ingrese el estado del usuario")]
        [DisplayName("Estado")]
        public bool estado { get; set; }
        public long id_rol_fk { get; set; }
        public long id_direccion_fk { get; set; }

        [ForeignKey("id_rol_fk")]
        public USUARIO_ROLES rol { get; set; }

        [ForeignKey("id_direccion_fk")]
        public USUARIO_DIRECCIONES direccion { get; set; }
    }

}
