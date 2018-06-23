using System;
using System.Linq;
using DecisionTech.Basket.DataAccess;
using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for managing the basket
    /// </summary>
    public class BasketService : IBasketService
    {
        /// <summary>
        /// The place where we store the baskets
        /// </summary>
        private readonly IStorageService _storageService;

        /// <summary>
        /// Repo where the products are defined
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// The service which responsible for all price calculations
        /// </summary>
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

        /// <summary>
        /// Adds a new item to a basket
        /// </summary>
        /// <param name="basketId">The unique basket ID</param>
        /// <param name="itemId">The Id of the item to be added.</param>
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
                    UnitPrice = product.Price
                });
            }

            this._storageService.Put(basketId, basket);
        }


        /// <summary>
        /// Calculates the total price of the basket
        /// </summary>
        /// <param name="basketId">The unique basket ID</param>
        /// <returns>Total price of the basket including discounts</returns>
        public decimal GetTotalPrice(string basketId)
        {
            var basket = this._storageService.Get<BasketModel>(basketId);

            if (basket == null)
            {
                throw new Exception($"The basket with the id '{basketId}' does not exist.");
            }

            return this._basketPricingService.CalculateBasketTotalPrice(basket);
        }

    }
}