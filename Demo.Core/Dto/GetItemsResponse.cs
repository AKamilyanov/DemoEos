using System.Collections.Generic;
using Demo.Core.Entities;
using Demo.Core.UseCases.CommonInterface;

namespace Demo.Core.Dto
{
    /// <summary>
    /// DTO response in GetItems use case
    /// </summary>
    public class GetItemsResponse : UseCaseResponseMessage
    {
        public IEnumerable<IItem> Items { get; }

        public GetItemsResponse(bool success, IEnumerable<IItem> items = null) : base(success)
        {
            Items = items ?? new List<IItem>();
        }
    }
}
