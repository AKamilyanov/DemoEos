using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Core.Entities;
using Demo.Data.Repositories;
using Xunit;

namespace Demo.Core.UnitTest
{
    public class ItemsGenerationUnitTest
    {

        private static int CountNodesHelper(int depth, int childsNum) =>
            (int)(depth == 1 ?
                Math.Pow(childsNum, depth) :
                Math.Pow(childsNum, depth) + CountNodesHelper(depth - 1, childsNum));

        // 
        [Fact]
        public void Data_Generated()
        {
            // arrange
            var testGenerationRule = new TestGenerationRule();
            ItemGenerator generator = new ItemGenerator(testGenerationRule);

            // act
            List<IItem> items = generator.GenerateItems();

            // assert
            int countOnFirstLevel = items.Count(item => item.Parent == null);
            Assert.True(countOnFirstLevel == testGenerationRule.ChildsOnLevel);
            Assert.True(items.All(item => item.MaxValue == testGenerationRule.MaxDepth));
            Assert.True(items.Count == CountNodesHelper(testGenerationRule.MaxDepth, testGenerationRule.ChildsOnLevel));
        }
    }
}
