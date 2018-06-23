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
        
        [HttpGet("totalprice/{basketId}")]
        public ActionResult<string> GetTotalPrice(string basketId)
        {
            return Ok(this._basketService.GetTotalPrice(basketId));
        }
        
        [HttpPut("{basketId}/{itemId}")]
        public void Put(string basketId, string itemId)
        {
            this._basketService.AddItem(basketId, itemId);
        }
    }
}
