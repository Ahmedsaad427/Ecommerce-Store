using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket == null)
            {
                throw new BasketNotFoundException(id);
            }

            var result = _mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDto basketDto)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket, TimeSpan.FromDays(30)); // Default TTL
            if (updatedBasket == null)
            {
                throw new BasketCreateOrUpdateBadRequestException();
            }

            return _mapper.Map<BasketDto>(updatedBasket);
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var flag = await _basketRepository.DeleteBasketAsync(basketId);
            if (flag == false)
            {
                throw new BasketDeleteBadRequestException();
            }

            else
            {
                return flag;
            }
        }
    }
}
