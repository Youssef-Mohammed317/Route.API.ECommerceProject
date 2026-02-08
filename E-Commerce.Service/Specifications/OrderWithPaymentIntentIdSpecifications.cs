using E_Commerce.Domian.Entites.OrderModule;

namespace E_Commerce.Service.Implementation.Specifications;

public class OrderWithPaymentIntentIdSpecifications : BaseSpecification<Order, int>
{
    public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
    {

    }
}
