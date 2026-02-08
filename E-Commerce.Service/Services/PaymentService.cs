using AutoMapper;
using E_Commerce.Domian.Entites.OrderModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Service.Implementation.Specifications;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Services;

public class PaymentService(IConfiguration config, IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
{
    public async Task<Result<BasketDTO>> CreateOrUpdatePaymentAsync(Guid BasketId)
    {
        // configure stripe => install stripe package
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
        // get basket
        var basketResult = await basketService.GetBasketAsync(BasketId);

        if (basketResult.IsFailure)
        {
            return Result<BasketDTO>.Fail(Error.NotFound(description: $"basket not found wiht id = {BasketId}"));
        }
        var basket = basketResult.Value;

        // get deliverymethod 
        if (basket.DeliveryMethodId == null) return Result<BasketDTO>.Fail(Error.NotFound(description: $"you must choose an delivery method"));
        var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
        basket.ShippingPrice = deliveryMethod.Price;
        //get amount(get product + delivery method cost)

        var productRepo = unitOfWork.GetRepository<E_Commerce.Domian.Entites.ProductModule.Product, int>();
        foreach (var item in basket.Items)
        {
            var product = await productRepo.GetByIdAsync(item.Id);
            if (product == null)
                return Result<BasketDTO>.Fail(Error.NotFound(description: $"Product not found with id = {item.Id}"));

            item.Price = product.Price;

        }
        var amount = (long)(basket.Items.Sum(c => c.Quantity * c.Price) + deliveryMethod.Price) * 100;

        // create payment intent [create-update] 

        var paymentService = new PaymentIntentService();
        if (basket.PaymentIntentId is null)
        {
            // create
            var options = new PaymentIntentCreateOptions();
            options.Amount = amount;
            options.Currency = "USD";
            options.PaymentMethodTypes = ["card"];


            var paymentIntent = await paymentService.CreateAsync(options);

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

          

        }
        else
        {
            // update
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = amount,

            };
            await paymentService.UpdateAsync(basket.PaymentIntentId, options: options);

        }
        await basketService.CreateOrUpdateBasketAsync(basket);

        return Result<BasketDTO>.Ok(mapper.Map<BasketDTO>(basket));

    }

    public async Task<Result> UpdateOrderPaymentStatusAsync(string request, string header)
    {
        var endPointSecret = config["StripeSettings:EndPointSecret"];

        if (endPointSecret == null)
        {
            return Result.Fail(Error.Failure());
        }

        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                request,
                header,
                endPointSecret
            );
        }
        catch (Exception ex)
        {
            return Result.Fail(Error.Failure(description: $"Webhook signature verification failed: {ex.Message}"));
        }


        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

        if (paymentIntent == null)
            return Result.Fail(Error.Failure(description: "PaymentIntent is null"));


        switch (stripeEvent.Type)
        {
            case "payment_intent.payment_failed":
                await UpdatePaymentFailed(paymentIntent.Id);
                break;

            case "payment_intent.succeeded":
                await UpdatePaymentReceived(paymentIntent.Id);
                break;

            default:
                // Events مش محتاجينها حاليًا
                break;
        }


        return Result.Ok();
    }

    private async Task UpdatePaymentReceived(string paymentIntentId)
    {
        var order = await unitOfWork.GetRepository<Order, int>().GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));


        if (order == null) return;

        order.OrderStatus = OrderStatus.PaymentReceived;

        unitOfWork.GetRepository<Order, int>().Update(order);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task UpdatePaymentFailed(string paymentIntentId)
    {
        var order = await unitOfWork.GetRepository<Order, int>().GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));


        if (order == null) return;

        order.OrderStatus = OrderStatus.PaymentFailed;

        unitOfWork.GetRepository<Order, int>().Update(order);
        await unitOfWork.SaveChangesAsync();
    }

}


