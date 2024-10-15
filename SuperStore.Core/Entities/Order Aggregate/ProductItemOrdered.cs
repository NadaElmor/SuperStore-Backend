﻿namespace SuperStore.Core.Entities.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered() { }
        public ProductItemOrdered(int productId, string productName, string productUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = productUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}