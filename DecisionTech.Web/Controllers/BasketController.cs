using System.Collections.Generic;
using DecisionTech.Basket.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace DecisionTech.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        /// <summary>
        /// Gets the total price of the specified basket
        /// </summary>
        /// <param name="basketId">The Id of the basket</param>
        /// <returns>Total price of the basket including discounts</returns>
        [HttpGet("totalprice/{basketId}")]
        public ActionResult<decimal> GetTotalPrice(string basketId)
        {
            var total = this._basketService.GetTotalPrice(basketId);

            return Ok(total);
        }

        /// <summary>
        /// Adds a new item to the specified basket.
        /// If the basket does not exist, a new one will be created.
        /// </summary>
        /// <param name="basketId">The id of the basket to be updated</param>
        /// <param name="itemId">The id of the item to be added to the basket (bread, butter or milk)</param>
        [HttpPut("{basketId}/{itemId}")]
        public void Put(string basketId, string itemId)
        {
            this._basketService.AddItem(basketId, itemId);
        }
    }
}
