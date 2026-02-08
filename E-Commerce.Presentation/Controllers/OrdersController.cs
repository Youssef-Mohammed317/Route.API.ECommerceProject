using E_Commerce.Presentation.Attributes;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.DTOs.OrderDTOs;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers;

public class OrdersController(IOrderService _orderService) : ApiBaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {

        var result = await _orderService.CreateOrder(orderDto, GetEmailFromToken());
        return FromResult(result);
    }
    [HttpGet("DeliveryMethods")]
    [Authorize]
    //[RedisCache(15)]
    public async Task<IActionResult> GetAllDeliveryMethods()
    {
        var result = await _orderService.GetAllDeliveryMethodAsync();
        return FromResult(result);
    }
    [HttpGet]
    [Authorize]
    //[RedisCache(15)]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _orderService.GetAllOrdersAsync(GetEmailFromToken());
        return FromResult(result);
    }
    [HttpGet("{id:int}")]
    [Authorize]
    //[RedisCache(15)]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        return FromResult(result);
    }

}
