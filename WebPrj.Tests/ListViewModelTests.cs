using WebPrj.Models;
using WebPrj.DAL.Entities;
using Xunit;

namespace WebPrj.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages() 
        {
            // Act - выполнение теста
            var model = ListViewModel<Laptop>.GetModel(TestData.GetlaptopsList(), 1, 3);

            // Assert - проверка того, что результат соответствует ожиданиям
            Assert.Equal(2, model.TotalPages);
        }

        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act - выполнение теста
            var model = ListViewModel<Laptop>.GetModel(TestData.GetlaptopsList(), page, 3);

            // Assert - проверка того, что результат соответствует ожиданиям
            Assert.Equal(qty, model.Count);
        }

        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act - выполнение теста
            var model = ListViewModel<Laptop>.GetModel(TestData.GetlaptopsList(), page, 3);

            // Assert - проверка того, что результат соответствует ожиданиям
            Assert.Equal(id, model[0].LaptopId);
        }
    }
}
