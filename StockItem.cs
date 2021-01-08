using System;

namespace Stationery_Management
{
    class StockItem
    {
        public string ItemCode { get; }
        public string ItemName { get; }
        public float Price { get; }
        public int Quantity { get; set; }

        public StockItem(string code, string name, float price, int quantity)
        {
            this.ItemCode = code;
            this.ItemName = name;
            this.Price = price;
            this.Quantity = quantity;
        }
    }
}
