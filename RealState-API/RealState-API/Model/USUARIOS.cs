using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class USUARIOS
    {
        [Key]
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONO { get; set; }
        public string CONTRASENNA { get; set; }
        public bool ESTADO { get; set; }
        public long ID_ROL_FK { get; set; }
        public long ID_DIRECCION_FK { get; set; }

        [ForeignKey("ID_ROL_FK")]
        public USUARIO_ROLES ROL { get; set; }

        [ForeignKey("ID_DIRECCION_FK")]
        public USUARIO_DIRECCIONES DIRECCION { get; set; }
    }

}
