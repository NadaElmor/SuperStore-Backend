using SuperStore.Core.Entities;

namespace SuperStore.DTOs
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
