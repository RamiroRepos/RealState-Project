namespace RealState_API.Models
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contrasenna { get; set; }            
        public bool Estado { get; set; }            
        public UsuarioRolesModel Roles { get; set; }
        public DireccionesModel DireccionesModel { get; set; }
    }
}
