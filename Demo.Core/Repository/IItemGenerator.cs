using System.Collections.Generic;
using Demo.Core.Entities;

namespace Demo.Core.Repository
{
    /// <summary>
    /// Interface for items generation
    /// </summary>
    public interface IItemGenerator
    {
        List<IItem> GenerateItems();
    }
}
