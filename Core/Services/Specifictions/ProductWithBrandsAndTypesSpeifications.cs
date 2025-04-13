using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifictions
{
    public class ProductWithBrandsAndTypesSpeifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpeifications(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }
        public ProductWithBrandsAndTypesSpeifications(int? brandId, int? typeId, string? sort) : base(
            P =>
            (!brandId.HasValue || P.BrandId == brandId)
            &&
            (!typeId.HasValue || P.TypeId == typeId)

            )
        {
            ApplyInclude();
            ApplySort(sort);
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
