using System.ComponentModel.DataAnnotations;

namespace eShopStudying.Models.ViewModels
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1, 10000, ErrorMessage = "Please enter a value between 1 and 10000")]
        public int Count { get; set; }
    }
}