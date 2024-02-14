using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using casazon.Data;
using casazon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace casazon.Views.Products
{
    public class IndexModel : PageModel
    {
        private readonly casazon.Data.casazonDbContext _context;

        public IndexModel(casazon.Data.casazonDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Names { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductName { get; set; }

        public async Task OnGetAsync()
        {
            var products = from m in _context.Products
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }

            Product = await products.ToListAsync();
        }
    }
}
