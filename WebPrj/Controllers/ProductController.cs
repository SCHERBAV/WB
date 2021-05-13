using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using WebPrj.DAL.Entities;

namespace WebPrj.Controllers
{
    public class ProductController : Controller
    {
        List<Laptop> _laptops;  //список ноутбуков
        List<Producer> _producers;  //список производителей

        public ProductController() 
        {
            InitData(); //инициализация списков
        }

        public IActionResult Index()
        {
            return View(_laptops);
        }

        //инициализация данных в списках
        public void InitData() 
        {
            _producers = new List<Producer>
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
            };

            _laptops = new List<Laptop>()
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
            };
        }
    }
}