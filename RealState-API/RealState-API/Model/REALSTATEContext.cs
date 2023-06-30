using Microsoft.EntityFrameworkCore;

namespace RealState_API.Model
{
    public class REALSTATEContext : DbContext
    {
        public REALSTATEContext(DbContextOptions<REALSTATEContext> opciones) : base(opciones)
        {
        }

        // Tablas de la Base de Datos
        public DbSet<PROPIEDADES> PROPIEDADES { get; set; }
        public DbSet<PROPIEDAD_TIPOS> PROPIEDAD_TIPOS { get; set; }
        public DbSet<PAISES> PAISES { get; set; }
        public DbSet<PROVINCIAS> PROVINCIAS { get; set; }
        public DbSet<USUARIO_ROLES> USUARIO_ROLES { get; set; }
        public DbSet<PROPIEDAD_DETALLES> PROPIEDAD_DETALLES { get; set; }
        public DbSet<USUARIO_DIRECCIONES> USUARIO_DIRECCIONES { get; set; }
        public DbSet<PROPIEDAD_DIRECCIONES> PROPIEDAD_DIRECCIONES { get; set; }
        public DbSet<USUARIOS> USUARIOS { get; set; }
        public DbSet<PROPIEDAD_IMAGENES> PROPIEDAD_IMAGENES { get; set; }
        public DbSet<BITACORAS> BITACORAS { get; set; }
        public DbSet<PROPIEDADES_CITAS> PROPIEDADES_CITAS { get; set; }
    }

}
