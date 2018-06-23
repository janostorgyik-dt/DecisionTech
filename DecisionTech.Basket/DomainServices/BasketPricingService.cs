using System;
using System.Linq;
using DecisionTech.Basket.DataAccess;
using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    public class BasketPricingService : IBasketPricingService
    {
        private IProductRepository _productRepository;

        public BasketPricingService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public decimal GetBasketTotalPrice(BasketModel basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            if (!basket.Items.Any())
            {
                return 0;
            }

            var milkDiscount = 0; //this.CalculateMilkDiscount(basket);
            var butterDiscount = this.CalculateButterDiscount(basket);

            return basket.Items.Sum(x => x.Quantity * x.UnitPrice) - milkDiscount - butterDiscount;

        }

        private decimal CalculateButterDiscount(BasketModel basket)
        {
            // Decide eligibility 
            if (ItemCount(basket, "butter") < 2 || ItemCount(basket, "bread") < 1)
            {
                return 0;
            }

            var discountMultiplier = Math.Floor(ItemCount(basket, "butter") / 2m);

            return Math.Min(discountMultiplier, ItemCount(basket, "bread")) * ItemPrice(basket, "bread") * .5m;
        }

        private static int ItemCount(BasketModel basket, string itemId)
        {
            return basket.Items.Single(x => string.Equals(x.ItemId, itemId, StringComparison.InvariantCultureIgnoreCase)).Quantity;
        }

        private static decimal ItemPrice(BasketModel basket, string itemId)
        {
            return basket.Items.Single(x => string.Equals(x.ItemId, itemId, StringComparison.InvariantCultureIgnoreCase)).UnitPrice;
        }

        private decimal CalculateMilkDiscount(BasketModel basket)
        {
            throw new NotImplementedException();
        }
    }
}