using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for calculating the total price of the basket including discounts
    /// </summary>
    public interface IBasketPricingService
    {
        /// <summary>
        /// Calculates the basket total
        /// </summary>
        /// <param name="basket">A basket which needs to be calculated</param>
        /// <returns>The total price of the basket, including discounts.</returns>
        decimal CalculateBasketTotalPrice(BasketModel basket);
    }

}
