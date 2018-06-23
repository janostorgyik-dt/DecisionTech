namespace DecisionTech.Basket.DomainObject
{
    public class BasketItemModel
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}