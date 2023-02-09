using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infra.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2);

            builder.HasData(
                new Product {
                    Id = 1,
                    Name = "Caderno",
                    Description = "Caderno espiral 100 fôlhas",
                    Price = 9.45m
                },
                new Product
                {
                    Id = 2,
                    Name = "Borracha",
                    Description = "Borracha branca",
                    Price = 0.75m
                },
                new Product
                {
                    Id = 3,
                    Name = "Lápis 12 cores",
                    Description = "Lápis de cor 12 cores",
                    Price = 18.30m
                }
            );
        } 
    }
}