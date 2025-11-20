using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Specifications.Orders;
using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork , IMapper _mapper,IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
            
            var basket =await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {

                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                if(product.Price != item.Price)item.Price = product.Price;


                var productInOrderItem = new ProductInOrderItem(item.Id,item.ProductName,item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                 orderItems.Add(orderItem);
            }


            var subTotal =  orderItems.Sum(OI=>OI.Price*OI.Quantity);


            var spec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            var existsOrder = await _unitOfWork.GetRepository<Guid,Order>().GetAsync(spec);
            if (existsOrder is not null)
                _unitOfWork.GetRepository<Guid, Order>().Delete(existsOrder);

            var order = new Order(userEmail, orderAddress,deliveryMethod,orderItems,subTotal,basket.PaymentIntentId);
            
            await  _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethod);
        } 

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id,userEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if(order is null) throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }
    }
}
