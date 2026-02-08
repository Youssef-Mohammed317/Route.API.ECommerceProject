using E_Commerce.Domian.Entites.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Specifications;

public class OrderSpecifications : BaseSpecification<Order, int>
{
    public OrderSpecifications(string email) : base(o => o.UserEmail.ToLower() == email.ToLower())
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.Items);
        AddOrderByDescending(o => o.OrderDate);
    }
    public OrderSpecifications(int id) : base(o => o.Id == id)
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.Items);
    }
}
