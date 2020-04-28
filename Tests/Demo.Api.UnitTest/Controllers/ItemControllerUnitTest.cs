using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Api.Controllers;
using Demo.Api.Presenters;
using Demo.Core.Dto;
using Demo.Core.Entities;
using Demo.Core.Repository;
using Demo.Core.UseCases;
using Demo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Demo.Api.UnitTest.Controllers
{
    public class ItemControllerUnitTest
    {
        [Fact]
        public async void Get_Items_Returns_Not_Empty_Collection()
        {
            // arrange
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemsAsync(It.IsAny<GetItemsRequest>()))
                .Returns(Task.FromResult(
                    new GetItemsResponse(true,
                    new List<IItem>
                    {
                        new Item
                        {
                            ItemId = Guid.NewGuid(),
                            Title = "Test",
                            Value = 1,
                            MaxValue = 1
                        }
                    })));
            
            var useCase = new GetItemsUseCase(repository.Object);
            var outputPort = new GetItemsPresenter();
            var controller = new ItemController(useCase, outputPort);
            var definition = new { Items = new List<Item>(), Success = true }; //to avoid interface deserialization and saving type

            // act
            var resultJson = await controller.GetItems(null,1,1);

            // assert
            var result = JsonConvert.DeserializeAnonymousType(((ContentResult)resultJson).Content, definition);
            Assert.True(result.Items.Count == 1);
        }
    }
}
