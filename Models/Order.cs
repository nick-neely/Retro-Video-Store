using System.ComponentModel.DataAnnotations;

namespace RetroVideoStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Customer email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public decimal TotalPrice { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}