using Demo.Core.Entities;

namespace Demo.Core.Repository
{
    /// <summary>
    /// Interface describes rule & parameters for items generation 
    /// </summary>
    public interface IGenerationRule
    {
        /// <summary>
        /// get number of childs on tree-level
        /// </summary>
        int ChildsOnLevel { get; }

        /// <summary>
        /// generate new item
        /// </summary>
        IItem GetItem(IItem parent);

        /// <summary>
        /// get max tree depth
        /// </summary>
        int MaxDepth { get; }

        /// <summary>
        /// max possible items count
        /// </summary>
        int MaxItemsCount { get; }
    }
}