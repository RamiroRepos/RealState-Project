namespace RealState_API.Models
{
    public class CitasModel
    {
        public long Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public UsuariosModel Usuario { get; set; }
        public PropiedadModel Propiedad { get; set; }

    }
}
