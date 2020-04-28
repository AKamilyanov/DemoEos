using System;

namespace Demo.Core.Entities
{
    public interface IItem
    {
        Guid ItemId { get; set; }

        IItem Parent { get; set; }

        string Title { get; set; }

        int Value { get; set; }

        int MaxValue { get; set; }
    }
}
