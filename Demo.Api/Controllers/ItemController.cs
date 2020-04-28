using System;
using System.Threading.Tasks;
using Demo.Api.Presenters;
using Demo.Core.Dto;
using Demo.Core.Entities;
using Demo.Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IGetItemsUseCase _getItemsUseCase;
        private readonly GetItemsPresenter _getItemsPresenter;
        
        public ItemController(IGetItemsUseCase getItemsUseCase, GetItemsPresenter getItemsPresenter)
        {
            _getItemsUseCase = getItemsUseCase;
            _getItemsPresenter = getItemsPresenter;
        }

        /// <summary>
        /// Найти все записи ParentId=pParentId, отсортировать полученные записи по полю Title, выдать в итоговом наборе массив размером  pPageSize с позиции pPage*pPageSize.
        /// </summary>
        /// <param name="pParentId">Идентификатор родительского узла, может быть null</param>
        /// <param name="pPageNum">Номер страницы (нумерация с 1)</param>
        /// <param name="pPageSize">Количество записей на странице (больше 0)</param>
        /// <response code="200">JSON, содержащий массив записей со следующими полями (ItemId, Title, MaxValue). Поле MaxValue = максимальное значение среди собственного значения Value и значений дочерних элементов</response>
        [HttpGet]
        [Route("GetItems")]
        [ProducesResponseType(typeof(IItem),200)]
        [Produces("application/json")]
        public async Task<ActionResult> GetItems(Guid? pParentId, int pPageNum, int pPageSize)
        {
            LogManager.GetCurrentClassLogger()
                .Trace($"GetItems pParentId={pParentId}, pPageNum={pPageNum}, pPageSize={pPageSize}");

            if (pPageNum < 1 || pPageSize < 1)
                return BadRequest(new { error = "Page number and page size should be greater or equal than 1" });

            await _getItemsUseCase.Handle(new GetItemsRequest(pParentId, pPageNum, pPageSize), _getItemsPresenter);

            return _getItemsPresenter.ContentResult;
        }
    }
}