namespace RealState_WEB.Models
{
    public class ImagenesPropiedadModel
    {
        public long IdImg1 { get; set; }
        public string ImgRuta1 { get; set; }
        public long IdImg2 { get; set; }
        public string ImgRuta2 { get; set; }
        public long IdImg3 { get; set; }
        public string ImgRuta3 { get; set; }
        public long IdImg4 { get; set; }
        public string ImgRuta4 { get; set; }
        //public HttpPostedFileBase[] Imagenes { get; set; }
        public List<IFormFile> Imagenes { get; set; }
        public IFormFile IMG1 { get; set; }
        public IFormFile IMG2 { get; set; }
        public IFormFile IMG3 { get; set; }
        public IFormFile IMG4 { get; set; }
    }
}
