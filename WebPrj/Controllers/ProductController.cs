using Microsoft.AspNetCore.Mvc;
//lb6.
using System.Linq;
using WebPrj.Models;
using WebPrj.DAL.Entities;
//lb8. Внедрение контекста БД в контроллер
using WebPrj.DAL.Data;
//lb7.
using WebPrj.Extensions;
//lb9. Для применения логера
using Microsoft.Extensions.Logging;

namespace WebPrj.Controllers
{
    public class ProductController : Controller
    {

        //lb8. 4.2
        ApplicationDbContext _context;  //контекст БД

        //lb6.
        int _pageSize;  //количество объектов на странице

        //lb9. 4.1.1 Применение логера
        private ILogger _logger; 


        //List<Laptop> _laptops;  //список ноутбуков

        //lb8. 4.2 Убрать поля
        //List<Producer> _producers;  //список производителей

        //lb6. Назначить поля
        //lb8. 4.2 Убрать поля
        //public List<Laptop> _laptops;  //список ноутбуков


        //public ProductController() 
        //lb8. 4.2.
        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            //lb8. 4.2 Убрать вызов инициализации полей
            //InitData(); //инициализация списков

            //lb6.
            _pageSize = 3;
            //lb8. 4.2
            _context = context;
            //lb9 4.1.1
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View(_laptops);
        //}


        //lb7. 4.6
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]

        //lb6.
        //public IActionResult Index(int pageNo = 1)
        //lb6. 4.6.2
        //public IActionResult Index(int? group, int pageNo = 1)
        public IActionResult Index(short? group, int pageNo)
        {
            var groupName = group.HasValue ? _context.Producers.Find(group.Value)?.ProducerName : "all groups";

            //lb9.
            _logger.LogInformation($"info: group={groupName}, page={pageNo}");

            //Skip - Обходит указанное количество элементов в последовательности, а затем возвращает оставшиеся элементы.
            //Take - Возвращает указанное количество смежных элементов с начала последовательности.

            //var items = _laptops.Skip((pageNo - 1) * _pageSize).Take(_pageSize).ToList();
            //return View(items);

            //ViewData["Groups"] = _producers;    // передача списка групп(производителей) представлению
            //lb8. 4.2
            ViewData["Groups"] = _context.Producers;    // передача списка групп(производителей) представлению
            ViewData["CurrentGroup"] = group ?? 0;  // получить id ткекущей группы и поместить в TempData
                                                    //return View(ListViewModel<Laptop>.GetModel(_laptops, pageNo, _pageSize));          
            //var laptopsFiltered = _laptops.Where(l => !group.HasValue || l.ProducerId == group.Value);
            var laptopsFiltered = _context.Laptops.Where(l => !group.HasValue || l.ProducerId == group.Value);

            //lb6. 4.4.3
            //return View(ListViewModel<Laptop>.GetModel(laptopsFiltered, pageNo, _pageSize));

            //lb7. 4.3.3
            // асинхронный запрос
            var model = ListViewModel<Laptop>.GetModel(laptopsFiltered, pageNo, _pageSize);
            //if (Request.Headers["x-requested-with"].ToString().ToLower().Equals("xmlhttprequest")) return PartialView("_listpartial", model);
            if(Request.IsAjaxRequest()) return PartialView("_listpartial", model);
            else return View(model);
            
        }

        //инициализация данных в списках
        public void InitData() 
        {
            /*_producers = new List<Producer>
            {
                new Producer()
                {
                    ProducerId = 1,
                    ProducerName = "Acer",
                    Country = "Тайвань"
                },
                new Producer()
                {
                    ProducerId = 2,
                    ProducerName = "Asus",
                    Country = "Тайвань"
                },
                new Producer()
                {
                    ProducerId = 3,
                    ProducerName = "Apple",
                    Country = "США"
                },
                new Producer()
                {
                    ProducerId = 4,
                    ProducerName = "Lenovo",
                    Country = "Китай"
                },
            };*/

            /*_laptops = new List<Laptop>()
            {
                new Laptop()
                {
                    LaptopId = 1,
                    Model = "Nitro 5 AN517-52-74G2",
                    Processor = "Intel Core i7",
                    RAM = "8 gb",
                    Graphics = "NVIDIA GeForce RTX 3060",
                    SSD="512 gb",
                    Image="Acer Nitro 5 AN517-52-74G2 NH.QAWEP.004.jpeg",
                    Price = 1730,
                    ProducerId = 1,
                    producer = _producers[0]
                },
                new Laptop()
                {
                    LaptopId = 2,
                    Model = "TUF Gaming Dash F15 FX516PM-HN130T",
                    Processor = "Intel Core i7",
                    RAM = "16 gb",
                    Graphics = "NVIDIA GeForce RTX 3060",
                    SSD="512 gb",
                    Image="ASUS TUF Gaming Dash F15 FX516PM-HN130T.jpeg",
                    Price = 1564,
                    ProducerId = 2,
                    producer = _producers[1]
                    
                },
                new Laptop()
                {
                    LaptopId = 3,
                    Model = "Macbook Pro M1 2020 MYD82",
                    Processor = "Apple M1",
                    RAM = "8 gb",
                    Graphics = "Apple M1 GPU",
                    SSD="256 gb",
                    Image="Apple Macbook Pro 13 M1 2020 MYD82.jpeg",
                    Price = 2007,
                    ProducerId = 3,
                    producer = _producers[2]
                },
                new Laptop()
                {
                    LaptopId = 4,
                    Model = "IdeaPad L340-15IRH Gaming 81LK00Q4RE",
                    Processor = "Intel Core i5",
                    RAM = "8 gb",
                    Graphics = "NVIDIA GeForce GTX 1050",
                    SSD="512 gb",
                    Image="Lenovo IdeaPad L340-15IRH Gaming 81LK00Q4RE.jpeg",
                    Price = 826,
                    ProducerId = 4,
                    producer = _producers[3]
                },
                new Laptop()
                {
                    LaptopId = 5,
                    Model = "TUF Gaming F15 FX506LI-HN012",
                    Processor = "Intel Core i5",
                    RAM = "8 gb",
                    Graphics = "NVIDIA GeForce GTX 1650 Ti",
                    SSD="512 gb",
                    Image="ASUS TUF Gaming F15 FX506LI-HN012.jpeg",
                    Price = 1181,
                    ProducerId = 2,
                    producer = _producers[1]
                },
                new Laptop()
                {
                    LaptopId = 6,
                    Model = "Aspire 3 A314-22-A5LQ NX.HVVER.005",
                    Processor = "AMD 3020e",
                    RAM = "4 gb",
                    Graphics = "Integrated",
                    HDD="500 gb",
                    Image="Acer Aspire 3 A314-22-A5LQ NX.HVVER.005.jpeg",
                    Price = 431,
                    ProducerId = 1,
                    producer = _producers[0]
                }
            };*/
        }
    }
}