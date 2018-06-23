using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DataAccess
{
    /// <summary>
    /// The product repo...
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Returns the produce with the specified ID, otherwise null
        /// </summary>
        /// <param name="id">The unique id of the product</param>
        /// <returns>A <see cref="ProductRecord"/> instance.</returns>
        ProductRecord Get(string id);
    }
}