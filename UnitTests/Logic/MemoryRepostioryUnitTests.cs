using FluentAssertions;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Logic
{
    public class MemoryRepostioryUnitTests
    {
        [Fact]
        public async Task When_GetAll_Then_ReturnsAllStoredItems()
        {
            // Arrange
            var repository = new MemoryRepository<TestingModel>(new TestingModel(1, "test1"), new TestingModel(2, "test2"),
                new TestingModel(3, "test3"));

            // Act
            var items = await repository.GetAll();

            // Assert
            items.Count().Should().Be(3);
            items.First().Id.Should().Be(1);
        }

        [MemberData(nameof(GetTestItems))]
        [InlineData(1, "test1")]
        [InlineData(2, "test2")]
        [ClassData(typeof(ClassDataSource))]
        [Theory]
        public async Task When_GetItemById_Then_ReturnSpecifiedItem(int id, string name)
        {
            // Arrange
            var repository = new MemoryRepository<TestingModel>(new TestingModel(1, "test1"), new TestingModel(2, "test2"),
                new TestingModel(3, "test3"), new TestingModel(4, "test4"));

            // Act
            var item = await repository.GetById(id);

            // Assert
            item.Should().NotBeNull();
            item.Id.Should().Be(id);
            item.Name.Should().Be(name);
        }

        [Fact]
        public async Task When_GetItemByNotExistingId_Then_ReturnNull()
        {
            // Arrange
            var repository = new MemoryRepository<TestingModel>(new TestingModel(1, "test1"), new TestingModel(2, "test2"),
                new TestingModel(3, "test3"));

            // Act
            var item = await repository.GetById(4);

            // Assert
            item.Should().BeNull();
        }

        class TestingModel : IModel
        {
            public TestingModel(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }

        public static IEnumerable<object[]> GetTestItems => new List<object[]>
        {
            new object[] { 3, "test3" }
        };

        public class ClassDataSource : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 4, "test4" };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
