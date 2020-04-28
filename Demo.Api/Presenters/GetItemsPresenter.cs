using System.Net;
using Demo.Core.Dto;
using Demo.Core.UseCases.CommonInterface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Demo.Api.Presenters
{
    /// <summary>
    /// Presenter pattern on an Api side
    /// </summary>
    public sealed class GetItemsPresenter : IOutputPort<GetItemsResponse>
    {
        public ContentResult ContentResult { get; }

        public GetItemsPresenter()
        {
            ContentResult = new ContentResult {ContentType = "application/json"};
        }

        public void Handle(GetItemsResponse response)
        {
            LogManager.GetCurrentClassLogger().Trace("Handle start, response={@response}", response);

            if (response.Success)
            {
                ContentResult.StatusCode = (int?) HttpStatusCode.OK;
                ContentResult.Content = JsonConvert.SerializeObject(response);
            }
            else
            {
                ContentResult.StatusCode = (int?) HttpStatusCode.InternalServerError;
                ContentResult.Content = "Something goes wrong :( See log for details";
            }

        }
    }
}
