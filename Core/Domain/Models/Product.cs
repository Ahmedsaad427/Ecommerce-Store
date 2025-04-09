using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; } // FK
        public int TypeId { get; set; } // FK

        // Navigation properties
        public ProductBrand productBrand { get; set; }


        public ProductType productType { get; set; }

    }
}
