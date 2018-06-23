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

            var milkDiscount = this.CalculateMilkDiscount(basket);
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
            return FindInBasket(basket, itemId) != null ? FindInBasket(basket, itemId).Quantity : 0;
        }

        private static decimal ItemPrice(BasketModel basket, string itemId)
        {
            return FindInBasket(basket, itemId) != null ? FindInBasket(basket, itemId).UnitPrice : 0;
        }

        private static BasketItemModel FindInBasket(BasketModel basket, string itemId)
        {
            return basket.Items.SingleOrDefault(x => string.Equals(x.ItemId, itemId, StringComparison.InvariantCultureIgnoreCase));
        }

        private decimal CalculateMilkDiscount(BasketModel basket)
        {
            // Decide eligibility 
            if (ItemCount(basket, "milk") < 4 )
            {
                return 0;
            }
            
            var discountMultiplier = Math.Floor(ItemCount(basket, "milk") / 4m);

            return Math.Min(discountMultiplier, ItemCount(basket, "milk")) * ItemPrice(basket, "milk");
        }
    }
}