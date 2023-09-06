using System.ComponentModel.DataAnnotations;

namespace BuyLogTracker.Api.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 100 characters.")]
        public string Description { get; set; } = string.Empty;

        public User User { get; set; } = new();
    }
}
