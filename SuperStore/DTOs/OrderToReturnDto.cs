using SuperStore.Core.Entities.Order_Aggregate;

namespace SuperStore.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string Status { get; set; } 
        public DateTimeOffset OrderDate { get; set; } 
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
