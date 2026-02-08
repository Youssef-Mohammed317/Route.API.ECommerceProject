using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.OrderDTOs;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrder(OrderDto orderDto, string email);
        Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethodAsync();
        Task<Result<IEnumerable<OrderToReturnDto>>> GetAllOrdersAsync(string email);
        Task<Result<OrderToReturnDto>> GetOrderByIdAsync(int id);
    }


}
