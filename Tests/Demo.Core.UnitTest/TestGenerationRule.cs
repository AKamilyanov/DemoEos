using System;
using Demo.Core.Entities;
using Demo.Core.Repository;
using Demo.Data.Repositories;

namespace Demo.Core.UnitTest
{
    public class TestGenerationRule : IGenerationRule
    {
        public int ChildsOnLevel => 6;
        public int MaxDepth => 4;
        public int MaxItemsCount => 10000;

        public IItem GetItem(IItem parent)
        {
            return new Item
            {
                ItemId = Guid.NewGuid(),
                Value = parent?.Value + 1 ?? 1,
                Parent = parent
            };
        }
    }
}