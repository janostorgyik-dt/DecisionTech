using System.Collections.Generic;

namespace DecisionTech.Basket.DomainObject
{
    public class BasketModel
    {
        public BasketModel()
        {
            this.Items = new List<BasketItemModel>();
        }
        public IList<BasketItemModel> Items { get; set; }
    }
}
