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
    public class DeleteModel : PageModel
    {
        private readonly WebPrj.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebPrj.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Laptop Laptop { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Laptop = await _context.Laptops.FirstOrDefaultAsync(m => m.LaptopId == id);

            if (Laptop == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Laptop = await _context.Laptops.FindAsync(id);

            if (Laptop != null)
            {
                _context.Laptops.Remove(Laptop);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
