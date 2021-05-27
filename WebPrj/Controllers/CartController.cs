using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPrj.DAL.Data;
using WebPrj.Extensions;
using WebPrj.Models;

namespace WebPrj.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;  // контекст БД
        private string cartKey = "cart";
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart) 
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey);
            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(short id, string returnUrl) 
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey);
            var item = _context.Laptops.Find(id);
            if (item != null) 
            {
                _cart.AddToCart(item);
                HttpContext.Session.Set<Cart>(cartKey, _cart);
            }
            return Redirect(returnUrl);
        }

        public IActionResult Delete(short id)
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}
