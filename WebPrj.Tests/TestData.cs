using System.Collections.Generic;
using WebPrj.DAL.Entities;

namespace WebPrj.Tests
{
    //lb6.
    public class TestData
    {
        public static List<Laptop> GetlaptopsList()
        {
            return new List<Laptop>
                {
                    new Laptop{ LaptopId = 1 },
                    new Laptop{ LaptopId = 2 },
                    new Laptop{ LaptopId = 3 },
                    new Laptop{ LaptopId = 4 },
                    new Laptop{ LaptopId = 5 }
                };
        }

        public static IEnumerable<object[]> Params() 
        {
            yield return new object[] { 1, 3, 1 }; //страница - 1, кол-во объектов на странице - 3, id первого объекта - 1
            yield return new object[] { 2, 2, 4 }; //страница - 2, кол-во объектов на странице - 2, id первого объекта - 4
        }
    }
}