using System;
using DecisionTech.Basket.DataAccess;
using DecisionTech.Basket.DomainObject;
using DecisionTech.Basket.DomainServices;
using Moq;
using NUnit.Framework;

namespace DecisionTech.Basket.UnitTest.DomainService
{
    [TestFixture]
    public class BasketPricingServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;

        private BasketPricingService subjet;

        [SetUp]
        public void SetUp()
        {
            this._productRepositoryMock = new Mock<IProductRepository>();

            this.subjet = new BasketPricingService(
                this._productRepositoryMock.Object
            );
        }

        [Test]
        public void GetBasketTotalPrice_WithNullBasket_ThrowsException()
        {
            // Arrange

            // Act & Assert
            Assert.That(() => this.subjet.GetBasketTotalPrice(null),
                Throws.Exception
                    .TypeOf<ArgumentNullException>()
                    .With.Property("ParamName")
                    .EqualTo("basket"));

        }

        [Test]
        public void GetBasketTotalPrice_WithEmptyBasket_ReturnsZero()
        {
            // Arrange
            var basket = new BasketModel();

            // Act
            var result = this.subjet.GetBasketTotalPrice(basket);

            // Assert
            Assert.AreEqual(0, result, "The basket price should be 0");

        }

        /*
        
            Butter £0.80
            Milk £1.15
            Bread £1.00

            Offers
            • Buy 2 Butter and get a Bread at 50% off
            • Buy 3 Milk and get the 4th milk for free

            Scenarios
            • Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total
            should be £2.95
            • Given the basket has 2 butter and 2 bread when I total the basket then the total should be
            £3.10
            • Given the basket has 4 milk when I total the basket then the total should be £3.45
            • Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total
            should be £9.00
         
         */

        [Test]
        [TestCase(1, 1, 1, 2.95)]
        public void GetBasketTotalPrice_WithEachItemInBasket_ReturnsTotalPrice(int milkQty, int breadQty, int butterQty,
            decimal expectedPrice)
        {
            // Arrange
            var basket = this.BuildBasket(milkQty, breadQty, butterQty);

            // Act
            var result = this.subjet.GetBasketTotalPrice(basket);

            // Assert
            Assert.AreEqual(expectedPrice, result, "Incorrect total price.");
        }



        private BasketModel BuildBasket(int milkQty, int breadQty, int butterQty)
        {
            //Butter £0.80
            //Milk £1.15
            //Bread £1.00

            var basket = new BasketModel();

            if (milkQty > 0)
            {
                basket.Items.Add(new BasketItemModel
                {
                    ItemId = "milk",
                    Quantity = milkQty,
                    UnitProce = 1.15m
                });
            }

            if (breadQty > 0)
            {
                basket.Items.Add(new BasketItemModel
                {
                    ItemId = "bread",
                    Quantity = breadQty,
                    UnitProce = 1.00m
                });
            }

            if (butterQty > 0)
            {
                basket.Items.Add(new BasketItemModel
                {
                    ItemId = "butter",
                    Quantity = butterQty,
                    UnitProce = 0.80m
                });
            }

            return basket;

        }





}
}
