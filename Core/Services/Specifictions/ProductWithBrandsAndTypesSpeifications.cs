using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifictions
{
    public class ProductWithBrandsAndTypesSpeifications: BaseSpecifications<Product,int>
    {
        public ProductWithBrandsAndTypesSpeifications(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }
        public ProductWithBrandsAndTypesSpeifications(int? brandId, int? typeId) : base(
            P=>
            (!brandId.HasValue || P.BrandId == brandId)
            &&
            (!typeId.HasValue || P.TypeId == typeId)

            )
        {
            ApplyInclude();
        }


        private void ApplyInclude()
        {
            AddInclude(P => P.productBrand);
            AddInclude(P=> P.productType);
        }
    }
}
