using System.ComponentModel.DataAnnotations;

namespace eShopStudying.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cover type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}