namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for managing the basket
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// Generates a unique basket ID
        /// </summary>
        /// <returns>A unique basket ID</returns>
        string Create();

        /// <summary>
        /// Adds a new item to the basket
        /// </summary>
        /// <param name="basketId">The unique basket ID</param>
        /// <param name="itemId">The Id of the item to be added.</param>
        void AddItem(string basketId, string itemId);

        /// <summary>
        /// Calculates the total price of the basket
        /// </summary>
        /// <param name="basketId">The unique basket ID</param>
        /// <returns>Total price of the basket including discounts</returns>
        decimal GetTotalPrice(string basketId);

    }
}
