﻿using System.ComponentModel.DataAnnotations;

namespace SuperStore.DTOs
{
    public class OrderDto
    {
        [Required]
        [EmailAddress]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
