using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using RetroVideoStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RetroVideoStore.Services
{
    public class CartService
    {
        private const decimal DvdPrice = 2.99m;
        private const decimal VhsPrice = 1.99m;
        private List<CartItem> cartItems = new List<CartItem>();
        private readonly ProtectedLocalStorage localStorage;
        
        public CartService(ProtectedLocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }
        
        public List<CartItem> GetCartItems()
        {
            return cartItems;
        }

        public void AddToCart(Movie movie)
        {
            var cartItem = cartItems.Find(item => item.Movie.Id == movie.Id);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem { Movie = movie, Quantity = 1 });
            }
            SaveCartToLocalStorage();
        }

        public void ClearCart()
        {
            cartItems.Clear();
            SaveCartToLocalStorage();
        }

        public decimal GetTotalPrice()
        {
            return cartItems.Sum(item => item.Movie.Format == "DVD" ? item.Quantity * DvdPrice : item.Quantity * VhsPrice);
        }

        public decimal GetPriceForFormat(string format)
        {
            return format == "DVD" ? DvdPrice : VhsPrice;
        }
        
        public async Task LoadCartFromLocalStorage()
        {
            var result = await localStorage.GetAsync<List<CartItem>>("cartItems");
            if (result.Success)
            {
                cartItems = result.Value ?? new List<CartItem>();
            }
        }

        private async void SaveCartToLocalStorage()
        {
            await localStorage.SetAsync("cartItems", cartItems);
        }
    }
}