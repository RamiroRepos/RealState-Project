using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RealState_WEB.Model
{
    public class USUARIOS
    {
        [Key]
        [DisplayName("Id")]
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
        [DisplayName("Confirmar Contraseña")]
        public string? confirmarContrasenna { get; set; }
        [Required(ErrorMessage = "*Favor ingrese el estado del usuario")]
        [DisplayName("Estado")]
        public bool estado { get; set; }
        public long id_rol_fk { get; set; } = 0;
        public long id_direccion_fk { get; set; } = 0;

        [ForeignKey("id_rol_fk")]
        public USUARIO_ROLES? rol { get; set; }

        [ForeignKey("id_direccion_fk")]
        public USUARIO_DIRECCIONES direccion { get; set; }
        [NotMapped]
        public int CodigoValidarSistema { get; set; }
        [NotMapped]
        public int CodigoValidarUsuario { get; set; }
        [NotMapped]
        public bool CodigoValidado { get; set; }
    }

}
