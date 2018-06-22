using System;
using System.Linq;
using DecisionTech.Basket.DataAccess;
using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    public class BasketService : IBasketService
    {
        private readonly IStorageService _storageService;

        private readonly IProductRepository _productRepository;

        private readonly IBasketPricingService _basketPricingService;

        public BasketService(
            IStorageService storageService, 
            IProductRepository productRepository, 
            IBasketPricingService basketPricingService)
        {
            this._storageService = storageService;
            this._productRepository = productRepository;
            _basketPricingService = basketPricingService;
        }
        public string Create()
        {
            return Guid.NewGuid().ToString();
        }

        public void AddItem(string basketId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(basketId))
            {
                throw new ArgumentNullException(nameof(basketId));
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            var basket = this._storageService.Get<BasketModel>(basketId) ?? new BasketModel();

            if (basket.Items.Any(x => x.ItemId == itemId))
            {
                basket.Items.Single(x => x.ItemId == itemId).Quantity++;
            }
            else
            {
                var product = this._productRepository.Get(itemId);

                if (product == null)
                {
                    throw new Exception($"The product with the name '{itemId}' does not exist.");
                }

                basket.Items.Add(new BasketItemModel
                {
                    ItemId = itemId,
                    Quantity = 1,
                    UnitProce = product.Price
                });
            }

            this._storageService.Put(basketId, basket);
        }

        public decimal GetTotalPrice(string basketId)
        {
            var basket = this._storageService.Get<BasketModel>(basketId);

            if (basket == null)
            {
                throw new Exception($"The basket with the id '{basketId}' does not exist.");
            }

            return this._basketPricingService.GetBasketTotalPrice(basket);
        }

    }
}