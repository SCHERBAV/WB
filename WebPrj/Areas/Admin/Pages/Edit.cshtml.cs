using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPrj.DAL.Data;
using WebPrj.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebPrj.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebPrj.DAL.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public EditModel(WebPrj.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        [BindProperty]
        public IFormFile Image { get; set; }

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

            ViewData["ProducerId"] = new SelectList(_context.Producers, "ProducerId", "ProducerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {
                var fileName = $"{Laptop.LaptopId}" + Path.GetExtension(Image.FileName);
                Laptop.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }

            _context.Attach(Laptop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaptopExists(Laptop.LaptopId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
            return RedirectToPage("./Index");
        }

        private bool LaptopExists(short id)
        {
            return _context.Laptops.Any(e => e.LaptopId == id);
        }
    }
}
