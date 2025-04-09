using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasOne(P => P.productBrand)
                            .WithMany()
                            .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.productType)
                            .WithMany()
                            .HasForeignKey(P => P.TypeId);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

        }
    }
}
