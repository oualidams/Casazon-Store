using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using casazon.Data;
using casazon.Models;

namespace casazon.Controllers
{
    [Authorize] // Require authentication for the entire controller
    public class CartController : Controller
    {
        private readonly casazonDbContext _context;

        public CartController(casazonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = _context.Carts
                               .Include(c => c.Items)
                               .ThenInclude(i => i.ProductId)
                               .FirstOrDefault(c => c.UserId == User.Identity.Name);
            return View(cart.Items);
        }

        [HttpPost] // This action requires authentication
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var cart = await _context.Carts
                                         .Include(c => c.Items)
                                         .FirstOrDefaultAsync(c => c.UserId == User.Identity.Name);
                if (cart == null)
                {
                    cart = new Cart { UserId = User.Identity.Name };
                    _context.Carts.Add(cart);
                }

                var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cartItem = new CartItem(product) ;
                    _context.CartItems.Add(cartItem);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
