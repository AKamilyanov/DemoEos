using System;
using System.Collections.Generic;
using Demo.Api.Presenters;
using Demo.Core.Dto;
using Demo.Core.Entities;
using Demo.Data.Repositories;
using Newtonsoft.Json;
using Xunit;

namespace Demo.Api.UnitTest.Presenters
{
    public class GetItemsPresenterUnitTest
    {
        [Fact]
        public void Returns_Not_Null_Result()
        {
            // arrange
            var presenter = new GetItemsPresenter();
            var definition = new { Items = new List<Item>(), Success = true }; //to avoid interface deserialization and saving type

            // act
            presenter.Handle(new GetItemsResponse(true, new List<IItem>{new Item {ItemId = Guid.NewGuid(), Title="", Value = 1, MaxValue = 1}}));
            
            // assert
            var result = JsonConvert.DeserializeAnonymousType(presenter.ContentResult.Content, definition);
            Assert.True(result.Items.Count == 1);
        }
    }
}
