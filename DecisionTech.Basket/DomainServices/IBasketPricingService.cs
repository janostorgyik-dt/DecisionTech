using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for calculating the total price of the basket including discounts
    /// </summary>
    public interface IBasketPricingService
    {

        decimal GetBasketTotalPrice(BasketModel basket);
    }

}
