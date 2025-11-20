using Store.G02.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Orders
{
    public class OrderSpecification : BaseSpecifications<Guid,Order>
    {
        public OrderSpecification(Guid id,string userEmail):base(O=>O.Id==id&& O.userEmail.ToLower() == userEmail.ToLower()) 
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
        }
        public OrderSpecification(string userEmail):base(O=>O.userEmail.ToLower() == userEmail.ToLower()) 
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
            AddOrderyByDescending(O => O.OrderDate);
        }
    }
}
