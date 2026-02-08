using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.OrderModule;

public class Order : BaseEntity<int>
{
    public string UserEmail { get; set; } = null!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus OrderStatus { get; set; }
    public ShippingAddress Address { get; set; } = null!;

    public string? PaymentIntentId { get; set; } = string.Empty;

    public decimal SubTotal { get; set; }
    public decimal Total => SubTotal + DeliveryMethod.Price;

    #region Relations
    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;
    [ForeignKey(nameof(DeliveryMethod))]
    public int DeliveryMethodId { get; set; }  // FK
    public virtual ICollection<OrderItem> Items { get; set; } = [];
    #endregion
}
