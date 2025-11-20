using Store.G02.Shared.Dtos.Orders;

namespace Store.G02.Services.Abstractions.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
        public string    DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
    }
}