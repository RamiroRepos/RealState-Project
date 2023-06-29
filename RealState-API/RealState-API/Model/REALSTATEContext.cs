using Microsoft.EntityFrameworkCore;

namespace RealState_API.Model
{
    public class REALSTATEContext : DbContext
    {
        public REALSTATEContext(DbContextOptions<REALSTATEContext> opciones) : base(opciones)
        {
        }

        //Agrega todas las tablas de la BD
        public DbSet<PROPIEDADES> PROPIEDADES { get; set; }
        public DbSet<PROPIEDAD_TIPOS> PROPIEDAD_TIPOS { get; set; }
    }
}
