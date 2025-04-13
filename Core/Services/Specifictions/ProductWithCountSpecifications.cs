using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifictions
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParameter productSpecifications) :base
            (
             P =>
            (!productSpecifications.BrandId.HasValue || P.BrandId == productSpecifications.BrandId)
            &&
            (!productSpecifications.TypeId.HasValue || P.TypeId == productSpecifications.TypeId)

            )
        {
            
        }
    }
}
