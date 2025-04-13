using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using static System.Net.WebRequestMethods;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return null; 
            }
            else
            {
                return $"{configuration[key: "BaseUrl"]}/{source.PictureUrl}"; // Assuming PictureUrl is a relative path
            }
        }
    }
}
    
        
    

