using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound
{
    public class ProductNotFoundException(int id) : NotFoundException($"Product with Id : {id} was not found ")
    {
    }
}
