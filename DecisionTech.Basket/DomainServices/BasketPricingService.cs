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

            return basket.Items.Sum(x => x.Quantity * x.UnitPrice);

        }
    }
}