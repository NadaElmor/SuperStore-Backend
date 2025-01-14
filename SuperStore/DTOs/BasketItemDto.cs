﻿using System.ComponentModel.DataAnnotations;

namespace SuperStore.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="value is not correct")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity can not be 0")]
        public int Quantity { get; set; }
    }
}
