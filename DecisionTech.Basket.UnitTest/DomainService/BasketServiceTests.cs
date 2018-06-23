using System;
using System.Collections.Generic;
using DecisionTech.Basket.DataAccess;
using DecisionTech.Basket.DomainObject;
using DecisionTech.Basket.DomainServices;
using Moq;
using NUnit.Framework;
using IStorageService = DecisionTech.Basket.DomainServices.IStorageService;

namespace DecisionTech.Basket.UnitTest.DomainService
{
    [TestFixture]
    public class BasketServiceTests
    {
        private Mock<IStorageService> _storageServiceMock;

        private Mock<IProductRepository> _productRepositoryMock;

        private Mock<IBasketPricingService> _basketPricingServiceMock;

        private BasketService subjet;

        [SetUp]
        public void SetUp()
        {
            this._storageServiceMock = new Mock<IStorageService>();
            this._productRepositoryMock = new Mock<IProductRepository>();
            this._basketPricingServiceMock = new Mock<IBasketPricingService>();

            this.subjet = new BasketService(
                this._storageServiceMock.Object,
                this._productRepositoryMock.Object,
                this._basketPricingServiceMock.Object
            );
        }

        #region AddItem

        [Test]
        public void AddItem_WithNullBaskerId_ThrowsException()
        {
            // Arrange
            var basketId = string.Empty;
            var itemId = string.Empty;

            // Act & Assert
            Assert.That(() => this.subjet.AddItem(basketId, itemId),
                Throws.Exception
                    .TypeOf<ArgumentNullException>()
                    .With.Property("ParamName")
                    .EqualTo("basketId"));
        }

        [Test]
        public void AddItem_WithNullItemId_ThrowsException()
        {
            // Arrange
            var basketId = "some-basketId";
            var itemId = string.Empty;

            // Act & Assert
            Assert.That(() => this.subjet.AddItem(basketId, itemId),
                Throws.Exception
                    .TypeOf<ArgumentNullException>()
                    .With.Property("ParamName")
                    .EqualTo("itemId"));
        }

        [Test]
        public void AddItem_WithExistingtemId_IncrementsQuantity()
        {
            // Arrange
            var basketId = "some-basketId";
            var itemId = "some-itemId";

            var basket  = new BasketModel
            {
                Items = new List<BasketItemModel>
                {
                    new BasketItemModel{ItemId = itemId, Quantity = 1}
                }
            };

            this._storageServiceMock.Setup(x => x.Get<BasketModel>(basketId)).Returns(basket);
            this._storageServiceMock.Setup(x => x.Put(basketId, It.IsAny<BasketModel>()));

            // Act
            this.subjet.AddItem(basketId, itemId);

            // Assert
            this._storageServiceMock.Verify(x => x.Put(basketId, It.Is<BasketModel>(y => y.Items[0].Quantity == 2)),
                Times.Once);
        }

        [Test]
        public void AddItem_WithInvalidItem_ThrowsException()
        {
            // Arrange
            var basketId = "some-basketId";
            var itemId = "some-itemId";

            this._storageServiceMock.Setup(x => x.Get<BasketModel>(basketId)).Returns((BasketModel)null);
            this._storageServiceMock.Setup(x => x.Put(basketId, It.IsAny<BasketModel>()));
            this._productRepositoryMock.Setup(x => x.Get(It.IsAny<string>())).Returns((ProductRecord)null);

            // Act & Assert
            Assert.That(() => this.subjet.AddItem(basketId, itemId),
                Throws.Exception
                    .TypeOf<Exception>()
                    .With.Message.EqualTo($"The product with the name '{itemId}' does not exist."));
        }

        [Test]
        public void AddItem_WithNewItem_AddNewItemToBasket()
        {
            // Arrange
            var basketId = "some-basketId";
            var itemId = "some-itemId";

            var product = new ProductRecord
            {
                Id = itemId,
                Price = 1.11m
            };

            this._storageServiceMock.Setup(x => x.Get<BasketModel>(basketId)).Returns((BasketModel)null);
            this._storageServiceMock.Setup(x => x.Put(basketId, It.IsAny<BasketModel>()));
            this._productRepositoryMock.Setup(x => x.Get(itemId)).Returns(product);

            // Act 
            this.subjet.AddItem(basketId, itemId);

            // Assert
            this._storageServiceMock.Verify(x => x.Put(basketId, It.Is<BasketModel>(y => y.Items[0].ItemId == itemId)),
                Times.Once);
        }


        #endregion

        #region GetTotalPrice

        [Test]
        public void GetTotalPrice_WithNoBasket_ThrowsException()
        {
            // Arrange
            var basketId = "some-basketId";

            this._storageServiceMock.Setup(x => x.Get<BasketModel>(basketId)).Returns((BasketModel)null);

            // Act & Assert
            Assert.That(() => this.subjet.GetTotalPrice(basketId),
                Throws.Exception
                    .TypeOf<Exception>()
                    .With.Message.EqualTo($"The basket with the id '{basketId}' does not exist."));
        }

        [Test]
        public void GetTotalPrice_WithValidBasket_ReturnsPrice()
        {
            // Arrange
            var basketId = "some-basketId";

            var basket = new BasketModel
            {
                Items = new List<BasketItemModel>
                {
                    new BasketItemModel {ItemId = "", Quantity = 1, UnitPrice = 1}
                }
            };

            this._storageServiceMock.Setup(x => x.Get<BasketModel>(basketId)).Returns(basket);
            this._basketPricingServiceMock.Setup(x => x.GetBasketTotalPrice(It.IsAny<BasketModel>())).Returns(3);

            // Act 
            var result = this.subjet.GetTotalPrice(basketId);

            // Assert
            Assert.AreEqual(3, result, "The basket total price is incorrect");
        }

        #endregion


    }
}
