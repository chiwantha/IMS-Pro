using System;
using System.Collections.Generic;

namespace IMS_Upgrade_To_C_
{
    internal class Cart
    {
        public class CartItem
        {
            public string Student_ID { get; set; }
            public string Class_ID { get; set; }
            public string Class_Name { get; set; }
            public string Grade { get; set; }
            public string Batch { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
            public string Amount { get; set; }
            public string PaymentMethod { get; set; }
        }

        private List<CartItem> cartItems;

        public Cart()
        {
            cartItems = new List<CartItem>();
        }

        public void AddItem(CartItem newItem)
        {
            cartItems.Add(newItem);
        }

        public void RemoveItem(string studentId, string classid, string month, string year)
        {
            CartItem itemToRemove = cartItems.Find(item =>
                item.Student_ID == studentId &&
                item.Class_ID == classid &&
                item.Month == month &&
                item.Year == year);

            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
            }
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (CartItem item in cartItems)
            {
                if (decimal.TryParse(item.Amount, out decimal price))
                {
                    total += price;
                }
            }

            return total;
        }

        public List<CartItem> GetCartItems()
        {
            return cartItems;
        }

        public void ClearCart()
        {
            cartItems.Clear();
        }

    }
}
