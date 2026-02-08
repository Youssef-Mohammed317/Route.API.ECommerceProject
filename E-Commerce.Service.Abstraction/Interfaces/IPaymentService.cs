using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Interfaces;

public interface IPaymentService
{
    Task<Result<BasketDTO>> CreateOrUpdatePaymentAsync(Guid BasketId);
    Task<Result> UpdateOrderPaymentStatusAsync(string request, string header);
}
