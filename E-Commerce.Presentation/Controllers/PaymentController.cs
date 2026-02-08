using E_Commerce.Service.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers;

public class PaymentController(IPaymentService paymentService) : ApiBaseController
{
    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<IActionResult> CreateOrUpdatePaymentIntentAsync(Guid basketId)
    {
        var res = await paymentService.CreateOrUpdatePaymentAsync(basketId);
        return FromResult(res);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> WebHook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var result = await paymentService.UpdateOrderPaymentStatusAsync(json, Request.Headers["Stripe-Signature"]!);
        return FromResult(result);
    }
}
