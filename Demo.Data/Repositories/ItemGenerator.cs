using System.Collections.Generic;
using Demo.Core.Entities;
using Demo.Core.Repository;

namespace Demo.Data.Repositories
{
    /// <summary>
    /// Generates number of Items with generation rule
    /// </summary>
    public class ItemGenerator : IItemGenerator
    {
        private readonly IGenerationRule _generationRule;
        private readonly List<IItem> _items = new List<IItem>();

        public ItemGenerator(IGenerationRule generationRule)
        {
            _generationRule = generationRule;
        }

        public List<IItem> GenerateItems()
        {
            GenerateInternal(1, null);

            return _items;
        }

        /// <summary>
        /// Generate items and save MaxValue parameter
        /// </summary>
        private void GenerateInternal(int depth, IItem parent)
        {
            if (_items.Count > _generationRule.MaxItemsCount || depth > _generationRule.MaxDepth)
                return;

            var childsCount = _generationRule.ChildsOnLevel; 
            for (int i = 0; i < childsCount; i++)
            {
                IItem item = _generationRule.GetItem(parent);
               
                // update MaxValue
                item.MaxValue = item.Value;
                IItem currentNode = item;
                while (currentNode != null)
                {
                    if (currentNode.Parent?.MaxValue < item.MaxValue)
                        currentNode.Parent.MaxValue = item.MaxValue;
                    else break;

                    currentNode = currentNode.Parent;
                }

                GenerateInternal(depth + 1, item);

                _items.Add(item); // use parent.Childs.Add() for tree-like structure
            }
        }
    }
}
