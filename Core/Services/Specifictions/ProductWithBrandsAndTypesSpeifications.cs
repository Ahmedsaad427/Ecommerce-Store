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
        public ProductWithBrandsAndTypesSpeifications() : base(null)
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
