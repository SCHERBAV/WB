using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPrj.DAL.Entities
{
    public class Producer
    {
        public short ProducerId { get; set; } //id производителя
        public string ProducerName { get; set; }
        public string Country { get; set; }
        
        //навигационное свойство
        public List<Laptop> Laptops { get; set; }

        public Producer()
        {
            Laptops = new List<Laptop>();
        }
    }
}
