using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPrj.DAL.Entities
{
    public class Laptop
    {
        public short LaptopId { get; set; } //id Ноутбука
   
        public string Model { get; set; }   //модель
        public string Processor { get; set; }   //процессор
        public string RAM { get; set; } //оперативная память
        public string Graphics { get; set; }    //видеокарта
        public string HDD { get; set; } //HDD
        public string SSD { get; set; } //SSD
        
        public string Image { get; set; }   //имя файла изображения
        public decimal Price { get; set; }  //цена

        //навигационные свойства
        public int ProducerId { get; set; } //производитель
        public Producer producer { get; set; }
    }
}
