using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPrj.Extensions;
using WebPrj.Models;

namespace WebPrj.Components
{
    public class CartViewComponent : ViewComponent
    {
        private Cart _cart;
        public CartViewComponent(Cart cart) 
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            //var cart = HttpContext.Session.Get<Cart>("cart");
            //return View(cart);

            return View(_cart);
        }
    }
}
