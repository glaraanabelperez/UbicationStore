

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Entities;

namespace Infraestructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<StoreEntity>
    {
        public void Configure(EntityTypeBuilder<StoreEntity> builder)
        {
            builder.HasKey(e => e.StoreId);
            builder.ToTable("StoreId");
            builder.Property(e => e.StoreId)
                .HasColumnType("long")
                .HasColumnName("StoreId");

            builder.Property(e => e.Name)
              .HasColumnName("Name");

            builder.Property(e => e.Coordinates)
            .HasColumnName("Direction"); 

            //builder.Property(e => e.Direction)
            //    .HasColumnName("Direction");

            //builder.Property(e => e.Latitude)
            //   .HasColumnName("Latitude"); 
            //builder.Property(e => e.Longitude)
            //    .HasColumnName("Longitude");


        }
    }
}
