using System;
using Demo.Core.Entities;
using Demo.Core.Repository;

namespace Demo.Data.Repositories
{
    /// <summary>
    /// Rule for upto 500k random items
    /// </summary>
    public class Random500KGenerationRule : IGenerationRule
    {
        public int MaxDepth => 10;
        public int MaxItemsCount => 500000;
        public int ChildsOnLevel => 1 + _random.Next(_maxChilds);

        public IItem GetItem(IItem parent)
        {
            return new Item
            {
                ItemId = Guid.NewGuid(),
                Title = $"{Guid.NewGuid().ToString("n").Substring(0, 8)}",
                Value = _random.Next(1000000),
                Parent = parent
            };
        }

        readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly int _maxChilds = 20;
    }
}