using System.ComponentModel.DataAnnotations.Schema;
using casazon.Areas.Identity.Data;
namespace casazon.Models
{
    
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        // Navigation property to the User
        public casazonUser User { get; set; } 

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

}
