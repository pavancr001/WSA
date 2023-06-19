using Wsa.FarmersMarket.Core;
using Wsa.FarmersMarket.Models;
using System.Collections.Generic;
using Xunit;
using Wsa.Farmermarket.Services.Implementation;

namespace Wsa.Farmermarket.Tests
{
    public class OfferProcessorUnitTests
    {
        [Fact]
        public void IsOfferApplicable_BOGO_ReturnsTrueIfCoffeeItemExists()
        {
            // Arrange
            var offerCode = Constants.OfferCodeBOGO;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOfferApplicable_BOGO_ReturnsFalseIfCoffeeItemDoesNotExist()
        {
            // Arrange
            var offerCode = Constants.OfferCodeBOGO;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsOfferApplicable_APPL_ReturnsTrueIfAppleCountIsThreeOrMore()
        {
            // Arrange
            var offerCode = Constants.OfferCodeAPPL;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOfferApplicable_APPL_ReturnsFalseIfAppleCountIsLessThanThree()
        {
            // Arrange
            var offerCode = Constants.OfferCodeAPPL;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsOfferApplicable_CHMK_ReturnsTrueIfChaiItemExists()
        {
            // Arrange
            var offerCode = Constants.OfferCodeCHMK;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemChai },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOfferApplicable_CHMK_ReturnsFalseIfChaiItemDoesNotExist()
        {
            // Arrange
            var offerCode = Constants.OfferCodeCHMK;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsOfferApplicable_APOM_ReturnsTrueIfOatmealAndAppleItemsExist()
        {
            // Arrange
            var offerCode = Constants.OfferCodeAPOM;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemOatMeal },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOfferApplicable_APOM_ReturnsFalseIfOatmealOrAppleItemsDoNotExist()
        {
            // Arrange
            var offerCode = Constants.OfferCodeAPOM;
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            var result = offer.IsOfferApplicable(offerCode, purchasedItems);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ApplyBuyOneGetOneFreeCoffeeOffer_AppliesBOGOToOddIndexedCoffeeItems()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyBuyOneGetOneFreeCoffeeOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeBOGO, purchasedItems[1].OfferCode);
            Assert.Equal(Constants.OfferCodeBOGO, purchasedItems[3].OfferCode);
        }

        [Fact]
        public void ApplyBuyOneGetOneFreeCoffeeOffer_DoesNotApplyBOGOToEvenIndexedCoffeeItems()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyBuyOneGetOneFreeCoffeeOffer(purchasedItems);

            // Assert
            Assert.Null(purchasedItems[0].OfferCode);
            Assert.Null(purchasedItems[2].OfferCode);
        }

        [Fact]
        public void ApplyBuyOneGetOneFreeCoffeeOffer_DoesNotApplyBOGOIfNoCoffeeItemsExist()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyBuyOneGetOneFreeCoffeeOffer(purchasedItems);

            // Assert
            Assert.All(purchasedItems, item => Assert.Null(item.OfferCode));
        }



        [Fact]
        public void ApplyApplesPriceDropOffer_AppliesPriceDropToAppleItemsWhenCountIs3OrMore()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyApplesPriceDropOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[0].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[1].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[2].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[3].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[4].OfferCode);
        }

        [Fact]
        public void ApplyApplesPriceDropOffer_DoesNotApplyPriceDropWhenCountIsLessThan3()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyApplesPriceDropOffer(purchasedItems);

            // Assert
            Assert.Null(purchasedItems[0].OfferCode);
            Assert.Null(purchasedItems[1].OfferCode);
        }

        [Fact]
        public void ApplyApplesPriceDropOffer_DoesNotApplyPriceDropIfNoAppleItemsExist()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemChai },
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyApplesPriceDropOffer(purchasedItems);

            // Assert
            Assert.All(purchasedItems, item => Assert.Null(item.OfferCode));
        }

        [Fact]
        public void ApplyApplesPriceDropOffer_DoesNotApplyPriceDropToApplesWithExistingAPOMOffer()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemApple, OfferCode = Constants.OfferCodeAPOM },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyApplesPriceDropOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeAPOM, purchasedItems[0].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[1].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[2].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[3].OfferCode);
            Assert.Equal(Constants.OfferCodeAPPL, purchasedItems[4].OfferCode);
        }


        [Fact]
        public void ApplyChaiMilkFreeOffer_AppliesFreeMilkToChaiItemWithoutExistingMilkItem()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyChaiMilkFreeOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeCHMK, purchasedItems[1].OfferCode);
        }

        [Fact]
        public void ApplyChaiMilkFreeOffer_AddsFreeMilkToPurchaseWhenNoMilkItemExists()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemChai }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyChaiMilkFreeOffer(purchasedItems);

            // Assert
            Assert.Equal(2, purchasedItems.Count);
            Assert.Equal(Constants.ItemMilk, purchasedItems[1].ItemType);
        }

        [Fact]
        public void ApplyChaiMilkFreeOffer_DoesNotAddFreeMilkWhenMilkItemAlreadyExists()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemChai },
            new Item { ItemType = Constants.ItemMilk }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyChaiMilkFreeOffer(purchasedItems);

            // Assert
            Assert.Equal(2, purchasedItems.Count);
            Assert.Equal(Constants.OfferCodeCHMK, purchasedItems[1].OfferCode);
        }

        [Fact]
        public void ApplyChaiMilkFreeOffer_DoesNotApplyFreeMilkWhenChaiItemDoesNotExist()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemCoffee }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyChaiMilkFreeOffer(purchasedItems);

            // Assert
            Assert.Single(purchasedItems);
            Assert.Null(purchasedItems[0].OfferCode);
        }


        [Fact]
        public void ApplyOatmealApplesDiscountOffer_Applies50PercentDiscountToAppleItemsWithOatmeal()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemOatMeal },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyOatmealApplesDiscountOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeAPOM, purchasedItems[1].OfferCode);
        }

        [Fact]
        public void ApplyOatmealApplesDiscountOffer_Applies50PercentDiscountToOatmealItemsWithApples()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemOatMeal },
            new Item { ItemType = Constants.ItemOatMeal },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple },
            new Item { ItemType = Constants.ItemApple }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyOatmealApplesDiscountOffer(purchasedItems);

            // Assert
            Assert.Equal(Constants.OfferCodeAPOM, purchasedItems[2].OfferCode);
            Assert.Equal(Constants.OfferCodeAPOM, purchasedItems[3].OfferCode);
        }

        [Fact]
        public void ApplyOatmealApplesDiscountOffer_DoesNotApplyDiscountWhenOatmealOrAppleItemsAreMissing()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemOatMeal }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyOatmealApplesDiscountOffer(purchasedItems);

            // Assert
            Assert.Null(purchasedItems[0].OfferCode);
        }

        [Fact]
        public void ApplyOatmealApplesDiscountOffer_DoesNotApplyDiscountWhenBothOatmealAndAppleItemsAreMissing()
        {
            // Arrange
            var purchasedItems = new List<Item>
        {
            new Item { ItemType = Constants.ItemCoffee },
            new Item { ItemType = Constants.ItemMilk }
        };
            var offer = new OfferProcessor();

            // Act
            offer.ApplyOatmealApplesDiscountOffer(purchasedItems);

            // Assert
            Assert.Equal(2, purchasedItems.Count);
            Assert.Null(purchasedItems[0].OfferCode);
            Assert.Null(purchasedItems[1].OfferCode);
        }


    }

}
