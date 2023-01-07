using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eShopStudying.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name ="List price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, 10000)]
        public double Price { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name ="Price for 51-100")]
        public double Price50 { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name ="Price for 100+")]
        public double Price100 { get; set; }
        [Required]
        [ValidateNever]
        [Display(Name ="Image")]
        public string ImageUrl { get; set; }
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Display(Name ="Cover type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}