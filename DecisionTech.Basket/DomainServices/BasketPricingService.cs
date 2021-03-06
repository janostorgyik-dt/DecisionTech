﻿using System;
using System.Linq;
using DecisionTech.Basket.Constant;
using DecisionTech.Basket.DomainObject;

namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for calculating the total price of the basket including discounts
    /// </summary>
    public class BasketPricingService : IBasketPricingService
    {
        #region Public

        /// <summary>
        /// Calculates the basket total
        /// </summary>
        /// <param name="basket">A basket which needs to be calculated</param>
        /// <returns>The total price of the basket, including discounts.</returns>
        public decimal CalculateBasketTotalPrice(BasketModel basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            if (!basket.Items.Any())
            {
                return 0;
            }

            // These could come from a promo service rather than implemented as privates, but since
            // the calc is simple, this should do
            var milkDiscount = this.CalculateMilkDiscount(basket);
            var butterDiscount = this.CalculateButterDiscount(basket);

            return basket.Items.Sum(x => x.Quantity * x.UnitPrice) - milkDiscount - butterDiscount;
        }
        

        #endregion

        #region Private

        private decimal CalculateMilkDiscount(BasketModel basket)
        {
            var milkQty = ItemQty(basket, Item.MilkId);

            // Decide eligibility 
            if (milkQty < 4)
            {
                return 0;
            }

            var discountMultiplier = Math.Floor(milkQty / 4m);

            return Math.Min(discountMultiplier, milkQty) * ItemPrice(basket, Item.MilkId);
        }

        private decimal CalculateButterDiscount(BasketModel basket)
        {
            var butterQty = ItemQty(basket, Item.ButterId);
            var breadQty = ItemQty(basket, Item.BreadId);

            // Decide eligibility 
            if (butterQty < 2 || breadQty < 1)
            {
                return 0;
            }
            
            var discountMultiplier = Math.Floor(butterQty / 2m);

            return Math.Min(discountMultiplier, breadQty) * ItemPrice(basket, Item.BreadId) * .5m;
        }

        private static int ItemQty(BasketModel basket, string itemId)
        {
            return FindInBasket(basket, itemId) != null ? FindInBasket(basket, itemId).Quantity : 0;
        }

        private static decimal ItemPrice(BasketModel basket, string itemId)
        {
            return FindInBasket(basket, itemId) != null ? FindInBasket(basket, itemId).UnitPrice : 0;
        }

        private static BasketItemModel FindInBasket(BasketModel basket, string itemId)
        {
            return basket.Items.SingleOrDefault(x => string.Equals(x.ItemId, itemId, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion
    }
}