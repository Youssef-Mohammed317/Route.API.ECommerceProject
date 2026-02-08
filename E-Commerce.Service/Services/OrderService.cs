using AutoMapper;
using E_Commerce.Domian.Entites.IdentityModule;
using E_Commerce.Domian.Entites.OrderModule;
using E_Commerce.Domian.Entites.ProductModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Domian.Interfaces.Identity;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.Exceptions;
using E_Commerce.Service.Implementation.Specifications;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs;
using E_Commerce.Shared.DTOs.OrderDTOs;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services;

public class OrderService(IBasketService _basketService, IMapper _mapper, IUnitOfWork _unitOfWork,IAddressRepository addressRepository,UserManager<ApplicationUser> userManager) : IOrderService
{
    public async Task<Result<OrderToReturnDto>> CreateOrder(OrderDto orderDto, string email)
    {

        // get address
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<OrderToReturnDto>.Fail(Error.NotFound(description: $"user not found"));
        }
        

        var address =await addressRepository.GetAddressAsync(user.Id);
        if(address == null)
        {
            return Result<OrderToReturnDto>.Fail(Error.NotFound(description: $"address not found"));
        }
        // get basket
        var basket = await _basketService.GetBasketAsync(orderDto.BasketId);

        if (basket.IsFailure)
        {
            return Result<OrderToReturnDto>.Fail(Error.NotFound(description: $"basket with id = {orderDto.BasketId} not found"));
        }
        if (string.IsNullOrEmpty(basket.Value.PaymentIntentId)) 
            return Result<OrderToReturnDto>.Fail(Error.Failure(description: $"there is an duplicated payment intent id"));

        var orderRepo = _unitOfWork.GetRepository<Order, int>();

        var orderSpec = new OrderWithPaymentIntentIdSpecifications(basket.Value.PaymentIntentId);

        var existingOrder = await orderRepo.GetByIdAsync(orderSpec);


        if (existingOrder != null)
        {
            orderRepo.Remove(existingOrder);
            await _unitOfWork.SaveChangesAsync();
        }


        // create orderItemList
        List<OrderItem> orderItems = new List<OrderItem>();

        var productRepo = _unitOfWork.GetRepository<Product, int>();

        foreach (var item in basket.Value.Items)
        {
            var product = await productRepo.GetByIdAsync(item.Id);
            if (product == null)
            {

                return Result<OrderToReturnDto>.Fail(Error.NotFound(description: $"product with id = {item.Id} not found"));
            }
            var orderItem = new OrderItem
            {

                Price = item.Price,
                Quantity = item.Quantity,
                Product = new ProductItemOrdered
                {
                    PictureUrl = item.PictureUrl,
                    ProductId = item.Id,
                    ProductName = product.Name,
                }
            };

            orderItems.Add(orderItem);

        }
        // get delivary methods
        var delivaryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
        if (delivaryMethod == null)
        {
            return Result<OrderToReturnDto>.Fail(Error.NotFound(description: $"delvary method with id = {orderDto.DeliveryMethodId} not found"));
        }
        // calc sub total
        var subtotal = orderItems.Sum(i => i.Quantity * i.Price);
        // create order
        var order = new Order
        {
            UserEmail = email,
            DeliveryMethod = delivaryMethod,
            SubTotal = subtotal,
            Address = new ShippingAddress
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                Street  = address.Street,
                City = address.City,
                Country = address.Country
            },
            Items = orderItems,
            PaymentIntentId = basket.Value.PaymentIntentId,
            DeliveryMethodId = basket.Value.DeliveryMethodId!.Value,
            
        };

        await _unitOfWork.GetRepository<Order, int>().AddAsync(order);

        if (await _unitOfWork.SaveChangesAsync() > 0)
        {
            return Result<OrderToReturnDto>.Ok(_mapper.Map<OrderToReturnDto>(order));

        }
        return Result<OrderToReturnDto>.Fail(Error.Failure(description: $"failed to create the order"));

    }


    public async Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethodAsync()
    {
        var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

        return Result<IEnumerable<DeliveryMethodDto>>.Ok(_mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods));
    }
    public async Task<Result<IEnumerable<OrderToReturnDto>>> GetAllOrdersAsync(string email)
    {

        var orderSpecifications = new OrderSpecifications(email);

        var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsync(specification: orderSpecifications);

        return Result<IEnumerable<OrderToReturnDto>>.Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
    }
    public async Task<Result<OrderToReturnDto>> GetOrderByIdAsync(int id)
    {
        var orderSpecifications = new OrderSpecifications(id);

        var order = await _unitOfWork.GetRepository<Order, int>().GetByIdAsync(specification: orderSpecifications);


        return Result<OrderToReturnDto>.Ok(_mapper.Map<OrderToReturnDto>(order));
    }
}
