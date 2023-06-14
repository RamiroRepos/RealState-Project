namespace RealState_API.Models
{
    public class PropiedadesCitasModel
    {
        public long Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public UsuariosModel Usuarios { get; set; }
        public PropiedadModel Propiedad { get; set; }
    }
}
