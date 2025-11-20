using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } 
        public IEnumerable<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
