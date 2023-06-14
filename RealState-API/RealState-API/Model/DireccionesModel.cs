namespace RealState_API.Models
{
    public class DireccionesModel
    {
        public int Id { get; set; }
        public string DireccionExacta { get; set; }
        public string Gmaps { get; set; }
        public PaisModel PaisModel { get; set; }
        public ProvinciaModel ProvinciaModel { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
    }
}
