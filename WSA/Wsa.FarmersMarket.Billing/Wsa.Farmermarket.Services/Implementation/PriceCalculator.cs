using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsa.Farmermarket.Services.Interfaces;
using Wsa.FarmersMarket.Core;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Implementation
{
    public class PriceCalculator : IPriceCalculator
    {
        public Tuple<decimal, IList<Item>> CalculatePrice(List<Item> purchasedItems)
        {
            decimal totalPrice = 0m;

            foreach (var item in purchasedItems)
            {
                switch (item.ItemType)
                {
                    case Constants.ItemApple:
                        item.Price = Constants.ApplePrice;
                        if (item.OfferCode == Constants.OfferCodeAPOM)
                        {
                            // Apply 50% discount pricing on Apples
                            item.DiscountedPrice = item.Price * Constants.DiscountPercentage50;
                            Console.WriteLine($"Apple cost changed to {item.DiscountedPrice} after applying 50% discount");
                            continue;
                        }

                        if (item.OfferCode == Constants.OfferCodeAPPL)
                        {
                            // Apply fixed price discount on each apple
                            item.DiscountedPrice = item.Price - Constants.DiscountedAppleFixedPrice;
                            Console.WriteLine("Applying fixed price discount on each apple");
                            continue;
                        }

                        break;

                    case Constants.ItemChai:
                        item.Price = Constants.ChaiPrice;
                        if (item.OfferCode == Constants.OfferCodeCHMK)
                        {
                            // Apply free milk for only 1 chai
                            item.DiscountedPrice = Constants.DiscountOnMilkPrice;
                            Console.WriteLine($"One Milk is Free, discounted the milk price {Constants.DiscountOnMilkPrice}");
                        }
                        break;

                    case Constants.ItemCoffee:
                        item.Price = Constants.CoffeePrice;
                        if (item.OfferCode == Constants.OfferCodeBOGO)
                        {
                            // Apply buy one get one offer on coffee
                            item.DiscountedPrice = item.Price;
                            Console.WriteLine("Applying Buy one get one offer on coffee");
                        }
                        break;

                    case Constants.ItemMilk:
                        item.Price = Constants.MilkPrice;
                        if (item.OfferCode == Constants.OfferCodeCHMK)
                        {
                            // Apply discount on milk price
                            item.DiscountedPrice = Constants.DiscountOnMilkPrice;
                            Console.WriteLine($"Milk price {Constants.MilkPrice} has been added");
                        }
                        break;

                    case Constants.ItemOatMeal:
                        item.Price = Constants.OatMealPrice;
                        Console.WriteLine($"Oatmeal with price {Constants.OatMealPrice} has been added");
                        break;
                }

                // Common logic after applying discounts or adding items
                totalPrice = purchasedItems.Sum(x => x.Price) - purchasedItems.Sum(y => y.DiscountedPrice);
            }
            return new Tuple<decimal, IList<Item>>(totalPrice, purchasedItems);
        }
    }
}
