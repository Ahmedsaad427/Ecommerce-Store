using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public class ProductResultDto
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; } // FK
        public int TypeId { get; set; } // FK
        public string BrandName { get; set; } 

        public string TypeName { get; set; } 
    }
}
