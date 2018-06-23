using System;
using System.Collections.Generic;
using System.Linq;
using DecisionTech.Basket.Constant;
using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DataAccess
{
    /// <summary>
    /// A simple product repo helper implementation.
    /// Helper class and not considered as part of the solution... (no unit test, etc...)
    /// </summary>
    public class ReadOnlyProductRepository : IProductRepository
    {
        private IList<ProductRecord> _repo;

        public ReadOnlyProductRepository()
        {
            _repo = new List<ProductRecord>
            {
                new ProductRecord {Id = Item.ButterId, Price = Item.ButterPrice},
                new ProductRecord {Id = Item.MilkId,   Price = Item.MilkPrice},
                new ProductRecord {Id = Item.BreadId,  Price = Item.BreadPrice}
            };
        }

        public ProductRecord Get(string id)
        {
            return this._repo.SingleOrDefault(x =>
                string.Equals(x.Id, id, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}