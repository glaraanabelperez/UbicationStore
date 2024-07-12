using Infraestructure.Entities;
using Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Abrazos.Persistence.Database
{
    public  class ApplicationDbContext : StoreDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<StoreEntity> Store { get; set; } = null!;
     
        protected override void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreEntity>().HasData(
               new StoreEntity { StoreId = 1, Name = "Farmacia cerca", Coordinates = new Point(-58.5221 ,- 34.62572 ) { SRID = 4326 } },
               new StoreEntity { StoreId = 2, Name = "Farmacia lejos 1", Coordinates = new Point(-58.39228 ,- 34.62541) { SRID = 4326 } },
               new StoreEntity { StoreId = 2, Name = "Farmacia lejos", Coordinates = new Point(-58.48857, -34.64707) { SRID = 4326 } }
           ); 
            modelBuilder.UseCollation("Modern_Spanish_CI_AS"); 
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            //-34.62705, -58.52291

        }
    }
}

