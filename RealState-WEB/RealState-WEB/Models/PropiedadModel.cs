namespace RealState_WEB.Models
{
    public class PropiedadModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
        public PropiedadTipoModel PropiedadTipo { get; set; }
        public UsuariosModel Usuario { get; set; }
        public PropiedadDetallesModel PropiedadDetallesModel { get; set; }
        public DireccionesModel DireccionesModel { get; set; }

        public ImagenesPropiedadModel imagenesPropiedad { get; set; }
        //Campo de citas
        public CitasEnt Cita { get; set; }
    }
}
