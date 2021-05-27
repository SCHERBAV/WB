using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPrj.DAL.Data;
using WebPrj.DAL.Entities;

namespace WebPrj.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebPrj.DAL.Data.ApplicationDbContext _context;

        public IndexModel(WebPrj.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Laptop> Laptop { get;set; }

        public async Task OnGetAsync()
        {
            Laptop = await _context.Laptops.ToListAsync();
        }
    }
}
