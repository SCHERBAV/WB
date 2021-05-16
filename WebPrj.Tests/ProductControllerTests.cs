using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using WebPrj.Controllers;
using WebPrj.DAL.Entities;
using Xunit;

namespace WebPrj.Tests
{
    //lb6. Модульный тест метода Index контроллера Product 
    public class ProductControllerTests
    {
        [Theory]

        //[InlineData(1, 3, 1)]   //страница - 1, кол-во объектов на странице - 3, id первого объекта - 1
        //[InlineData(2, 2, 4)]   //страница - 2, кол-во объектов на странице - 2, id первого объекта - 4

        //тоже самое что и закоментированные строки выше, только источник данных - метод
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]    
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange - подготовка исходных данных
            var controller = new ProductController();

            //controller._laptops = new List<Laptop>
            //{
            //    new Laptop{ LaptopId = 1 },
            //    new Laptop{ LaptopId = 2 },
            //    new Laptop{ LaptopId = 3 },
            //    new Laptop{ LaptopId = 4 },
            //    new Laptop{ LaptopId = 5 }
            //};
            controller._laptops = TestData.GetlaptopsList();

            // Act - выполнение теста
            //var result = controller.Index() as ViewResult;
            var result = controller.Index(pageNo:page, group:null) as ViewResult;
            var model = result?.Model as List<Laptop>;

            // Assert - проверка того, что результат соответствует ожиданиям
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].LaptopId);

        }

        [Fact]
        public void ControllerSelectGroup() 
        {
            // Arrange - подготовка исходных данных
            var controller = new ProductController();
            var data = TestData.GetlaptopsList();
            controller._laptops = data;

            var comparer = Comparer<Laptop>.GetComparer((d1, d2) => d1.LaptopId.Equals(d2.LaptopId));

            // Act - выполнение теста
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<Laptop>;

            // Assert - проверка того, что результат соответствует ожиданиям
            Assert.Equal(1, model.Count);
            Assert.Equal(data[1], model[0], comparer);

        }
    }
}