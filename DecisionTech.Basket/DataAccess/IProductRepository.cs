using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DataAccess
{
    /// <summary>
    /// Responsible for storing things
    /// </summary>
    public interface IProductRepository
    {
        ProductRecord Get(string id);
    }
}