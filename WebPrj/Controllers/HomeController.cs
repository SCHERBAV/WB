using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebPrj.Classes_lb2;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPrj.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> listDemos;

        public IActionResult Index()
        {
            ViewData["Text"] = "Лабораторная работа 5"; 
            ViewData["Lst"] = new SelectList(listDemos, "ListItemValue", "ListItemText");
            ViewData["test"] = "test string";
            return View();
        }

        public HomeController()
        {
            listDemos = new List<ListDemo>{
                new ListDemo{ListItemValue=1, ListItemText="Item_1"},
                new ListDemo{ListItemValue=2, ListItemText="Item_2"},
                new ListDemo{ListItemValue=3, ListItemText="Item_3"}
            };
        }
    }
}