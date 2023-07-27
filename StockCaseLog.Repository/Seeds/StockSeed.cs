using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockCaseLog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Repository.Seeds
{
    public class StockSeed : IEntityTypeConfiguration<Stock>
    {
        private readonly int[] _ids;

        public StockSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasData(
                new Stock
                {
                    Id = 1,
                    ProductCode = "P001AD",
                    Quantity = 100,
                    VariantCode = "V002CAS",
                    IsActive = true
                },
                new Stock
                {
                    Id = 2,
                    ProductCode = "P002AD",
                    Quantity = 200,
                    VariantCode = "V002CAS",
                    IsActive = true
                },
                new Stock
                {
                    Id = 3,
                    ProductCode = "P003AD",
                    Quantity = 67,
                    VariantCode = "V003CAS",
                    IsActive = true
                },
                new Stock
                {
                    Id = 4,
                    ProductCode = "P004AD",
                    Quantity = 6,
                    VariantCode = "V004CAS",
                    IsActive = true
                }
                );
        }
    }
}
