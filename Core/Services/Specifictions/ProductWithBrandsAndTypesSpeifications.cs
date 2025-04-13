using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifictions
{
    public class ProductWithBrandsAndTypesSpeifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpeifications(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }
        public ProductWithBrandsAndTypesSpeifications(ProductSpecificationsParameter productSpecifications) : base(
            P =>
            (!productSpecifications.BrandId.HasValue || P.BrandId == productSpecifications.BrandId)
            &&
            (!productSpecifications.TypeId.HasValue || P.TypeId == productSpecifications.TypeId)

            )
        {
            ApplyInclude();
            ApplySort(productSpecifications.Sort);
            ApplyPagination(productSpecifications.PageIndex, productSpecifications.PageSize);
        }


        private void ApplyInclude()
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
        }

        private void ApplySort(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(P => P.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }
    }
}
