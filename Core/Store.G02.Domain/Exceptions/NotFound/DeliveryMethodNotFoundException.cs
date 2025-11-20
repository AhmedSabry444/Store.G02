using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"Delivery Method With Id: {id} Not Found")
    {
    }
}
