using System;
using Demo.Core.UseCases.CommonInterface;

namespace Demo.Core.Dto
{
    /// <summary>
    /// DTO request in GetItems use case
    /// </summary>
    public class GetItemsRequest : IUseCaseRequest<GetItemsResponse>
    {
        public Guid? ParentGuid { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetItemsRequest(Guid? parentGuid, int pageNumber, int pageSize)
        {
            ParentGuid = parentGuid;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
