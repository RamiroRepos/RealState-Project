﻿using System.ComponentModel.DataAnnotations;

namespace RealState_API.Model
{
    public class PROPIEDAD_TIPOS
    {
        [Key]
        public long ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
