using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebPrj.Models;

namespace WebPrj.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private List<MenuItem> listMenuItems = new List<MenuItem>(){
            new MenuItem(){ NameController="Action", Action="Index", Text="Lab_7" },
            new MenuItem(){ NameController="Product", Action="Index", Text="Каталог" },
            new MenuItem(){ IsRazorPageOrControllerMethod=true, NameArea="Admin", NamePage="/Index", Text="Администрирование"},
        };

        public IViewComponentResult Invoke()
        {
            //получение значений сегментов маршрута
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];

            foreach (var item in listMenuItems)
            {
                var EqualsController = controller?.Equals(item.NameController) ?? false; //название контроллера совпадает?
                var EqualsArea = area?.Equals(item.NameArea) ?? false;  //название зоны совпадает?
                // Если есть совпадение, то сделать элемент меню активным (применить соответствующий класс CSS)
                if (EqualsArea || EqualsController) item.NameCSSActive = "active";
            }
            return View(listMenuItems); //возвращаем результат в представление
        }
    }
}