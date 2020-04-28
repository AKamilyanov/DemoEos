using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Dto;
using Demo.Core.Entities;
using Demo.Core.Repository;
using Demo.Core.UseCases;
using Demo.Core.UseCases.CommonInterface;
using Demo.Data;
using Demo.Data.Factories;
using Demo.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Demo.Core.UnitTest
{
    public class GetItemsUnitTest
    {
        class TestRepositoryContextFactory : IRepositoryContextFactory
        {
            public RepositoryContext CreateDbContext()
            {
                var options = new DbContextOptionsBuilder<RepositoryContext>()
                    .UseInMemoryDatabase(databaseName: "TestItems")
                    .Options;
                
                var context = new RepositoryContext(options);
                
                return context;
            }
        }
        

        [Fact]
        public async void Repo_Returns_Top_Level_Items()
        {
            // arrange
            IItemRepository itemRepository = new ItemRepository(new TestRepositoryContextFactory());
            IGenerationRule testGenerationRule = new TestGenerationRule();
            await itemRepository.FillRepositoryAsync(new ItemGenerator(testGenerationRule));

            // act
            var result = await itemRepository.GetItemsAsync(new GetItemsRequest(null, 1, testGenerationRule.ChildsOnLevel));

            // assert
            Assert.True(result.Items.Count() == testGenerationRule.ChildsOnLevel);
            Assert.True(result.Items.All(item => item.MaxValue == testGenerationRule.MaxDepth));
        }

        [Fact]
        public async void Repo_Returns_Second_Page()
        {
            // arrange
            IItemRepository itemRepository = new ItemRepository(new TestRepositoryContextFactory());
            IGenerationRule testGenerationRule = new TestGenerationRule();
            await itemRepository.FillRepositoryAsync(new ItemGenerator(testGenerationRule));

            // act
            var result = await itemRepository.GetItemsAsync(new GetItemsRequest(null, 2, testGenerationRule.ChildsOnLevel/2));

            // assert
            Assert.True(result.Items.Count() == testGenerationRule.ChildsOnLevel/2);
            Assert.True(result.Items.All(item => item.MaxValue == testGenerationRule.MaxDepth));
        }

        [Fact]
        public async void Repo_Measure_Depth_Correct()
        {
            // arrange
            IItemRepository itemRepository = new ItemRepository(new TestRepositoryContextFactory());
            IGenerationRule testGenerationRule = new TestGenerationRule();
            await itemRepository.FillRepositoryAsync(new ItemGenerator(testGenerationRule));
            int depthCounter = 0;

            // act
            var result = await itemRepository.GetItemsAsync(new GetItemsRequest(null, 1, testGenerationRule.ChildsOnLevel));
            while (result.Items.Any())
            {
                IItem item = result.Items.First();
                result = await itemRepository.GetItemsAsync(new GetItemsRequest(item.ItemId, 1, testGenerationRule.ChildsOnLevel));

                depthCounter++;
            }

            Assert.True(depthCounter == testGenerationRule.MaxDepth);
        }

        [Fact]
        public async void Repo_Check_Bad_Arguments_1()
        {
            // arrange
            IItemRepository itemRepository = new ItemRepository(new TestRepositoryContextFactory());
            await itemRepository.FillRepositoryAsync(new ItemGenerator(new TestGenerationRule()));

            // act
            var result = await itemRepository.GetItemsAsync(new GetItemsRequest(Guid.NewGuid(), -555, -777));

            // assert
            Assert.True(!result.Items.Any());
        }

        [Fact]
        public async void Repo_Check_Bad_Arguments_2()
        {
            // arrange
            IItemRepository itemRepository = new ItemRepository(new TestRepositoryContextFactory());
            await itemRepository.FillRepositoryAsync(new ItemGenerator(new TestGenerationRule()));

            // act
            var result = await itemRepository.GetItemsAsync(new GetItemsRequest(null, 100, 100));

            // assert
            Assert.True(!result.Items.Any());
        }

       
        /// <summary>
        /// Test that getItems will return expected values
        /// </summary>
        [Fact]
        public async void GetItems_Request_Handled()
        {
            var mockItemRepository = new Mock<IItemRepository>();
            mockItemRepository
                .Setup(repo => repo.GetItemsAsync(It.IsAny<GetItemsRequest>()))
                .Returns(Task.FromResult(new GetItemsResponse(true, new List<IItem> {new Item()})));
            
            var useCase = new GetItemsUseCase(mockItemRepository.Object);

            var mockOutputPort =  new Mock<IOutputPort<GetItemsResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort.Handle(It.IsAny<GetItemsResponse>())).Verifiable();

            //act
            await useCase.Handle(new GetItemsRequest(null, 1, 1), mockOutputPort.Object);

            // assert
            mockOutputPort.Verify(port => port.Handle(It.IsAny<GetItemsResponse>()), Times.Once);
            
        }
    }
}
