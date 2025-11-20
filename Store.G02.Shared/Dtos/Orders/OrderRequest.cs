using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.Dtos.Orders
{
    public class OrderRequest
    {
        public string BasketId { get; set; } 
        public int DeliveryMethodId { get; set; }
         public OrderAddressDto ShipToAddress { get; set; }
    }
}
