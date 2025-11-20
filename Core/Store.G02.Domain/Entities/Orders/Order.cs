using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {

        public Order() 
        {

        }
        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal, string? paymentIntentId = null)
        {
            this.userEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string userEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; } 

        public DeliveryMethod DeliveryMethod { get; set; }   
        public int DeliveryMethodId { get; set; }   
        public ICollection<OrderItem> Items { get; set; } 
        public decimal Subtotal { get; set; }

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;

        public string? PaymentIntentId { get; set; }  
        }
}
