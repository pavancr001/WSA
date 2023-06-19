using Wsa.FarmersMarket.Core;
using Wsa.FarmersMarket.Models;
using System.Collections.Generic;
using Xunit;
using Wsa.Farmermarket.Services.Implementation;

namespace Wsa.Farmermarket.Tests
{
    public class PriceCalculatorUnitTestCases
    {        

        [Fact]
        public void CalculatePrice_CalculatesTotalPriceWithoutDiscounts()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.CalculatePrice(purchasedItems);

            // Assert
            Assert.Equal(20.34m, result.Item1);
            Assert.Equal(purchasedItems, result.Item2);
        }

        [Fact]
        public void CalculatePrice_CalculatesTotalPriceWithDiscounts()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple, OfferCode = Constants.OfferCodeAPOM },
            new Item { ItemType = Constants.ItemApple, OfferCode = "" },
            new Item { ItemType = Constants.ItemChai, OfferCode ="" },
            new Item { ItemType = Constants.ItemCoffee, OfferCode = "" },
            new Item { ItemType = Constants.ItemCoffee, OfferCode = Constants.OfferCodeBOGO },
            new Item { ItemType = Constants.ItemMilk, OfferCode = Constants.OfferCodeCHMK },
            new Item { ItemType = Constants.ItemOatMeal }
        };
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.CalculatePrice(purchasedItems);

            // Assert
            Assert.Equal(27.03m, result.Item1);
            Assert.Equal(purchasedItems, result.Item2);
        }

        [Fact]
        public void CalculatePrice_CalculatesTotalPriceWithDiscountsAndAdditionalItems()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple, OfferCode = Constants.OfferCodeAPOM },
            new Item { ItemType = Constants.ItemChai },
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemMilk, OfferCode = Constants.OfferCodeCHMK },
            new Item { ItemType = Constants.ItemMilk },
            new Item { ItemType = Constants.ItemOatMeal },
            new Item { ItemType = Constants.ItemOatMeal }
        };
            var calculator = new PriceCalculator();

            // Act
            var result = calculator.CalculatePrice(purchasedItems);

            // Assert
            Assert.Equal(29.47m, result.Item1);
            Assert.Equal(purchasedItems, result.Item2);
        }
    }
}
