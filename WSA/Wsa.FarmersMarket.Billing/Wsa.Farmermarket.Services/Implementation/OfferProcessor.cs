using Wsa.FarmersMarket.Core;
using Wsa.Farmermarket.Services.Interfaces;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Implementation
{
    public class OfferProcessor : IOfferProcessor
    {
        //public Dictionary<string, decimal> Items = new Dictionary<string, decimal>() {
        //    {"CF1", 11.23m },
        //    {"AP1", 6.0m },
        //    {"CH1", 3.11m },
        //    {"OM1", 3.690m },
        //    {"MK1", 4.75m }};
        public void ApplyOffer(string offerCode, List<Item> purchasedItems)
        {
            bool isApplicable = IsOfferApplicable(offerCode, purchasedItems);

            if (isApplicable)
            {
                switch (offerCode)
                {
                    case Constants.OfferCodeBOGO:
                        ApplyBuyOneGetOneFreeCoffeeOffer(purchasedItems);
                        break;
                    case Constants.OfferCodeAPPL:
                        ApplyApplesPriceDropOffer(purchasedItems);
                        break;
                    case Constants.OfferCodeCHMK:
                        ApplyChaiMilkFreeOffer(purchasedItems);
                        break;
                    case Constants.OfferCodeAPOM:
                        ApplyOatmealApplesDiscountOffer(purchasedItems);
                        break;
                    default:
                        Console.WriteLine("Invalid offer code");
                        break;
                }
            }
        }

        public bool IsOfferApplicable(string offerCode, List<Item> purchasedItems)
        {
            // Add the logic to check if the offer is applicable based on offer code and purchased items
            // Return true if the offer is applicable, false otherwise
            bool isApplicable = false;

            switch (offerCode)
            {
                case Constants.OfferCodeBOGO:
                    // Offer is applicable if there is at least one coffee item in the purchased items
                    isApplicable = purchasedItems.Exists(item => item.ItemType == Constants.ItemCoffee);
                    break;
                case Constants.OfferCodeAPPL:
                    // Offer is applicable if there are 3 or more apple items in the purchased items
                    int appleCount = purchasedItems.FindAll(item => item.ItemType == Constants.ItemApple).Count;
                    isApplicable = appleCount >= 3;
                    break;
                case Constants.OfferCodeCHMK:
                    // Offer is applicable if there is at least one Chai , Add the Milk item if does not exist in purchased items
                    bool hasChaiItem = purchasedItems.Exists(item => item.ItemType == Constants.ItemChai);
                    isApplicable = hasChaiItem;
                    break;
                case Constants.OfferCodeAPOM:
                    // Offer is applicable if there is at least one Oatmeal item and one Apple item in the purchased items
                    bool hasOatmealItem = purchasedItems.Exists(item => item.ItemType == Constants.ItemOatMeal);
                    bool hasAppleItem = purchasedItems.Exists(item => item.ItemType == Constants.ItemApple);
                    isApplicable = hasOatmealItem && hasAppleItem;
                    break;
                default:
                    isApplicable = false; // Default to false if offer code is not recognized
                    break;
            }

            return isApplicable;
        }

        public void ApplyBuyOneGetOneFreeCoffeeOffer(List<Item> purchasedItems)
        {
            Console.WriteLine("Applying Buy-One-Get-One-Free Special on Coffee.");

            // Find all coffee items in the purchased items
            List<Item> coffeeItems = purchasedItems.FindAll(item => item.ItemType == Constants.ItemCoffee);

            for (int index = 0; index < coffeeItems.Count; index++)
            {
                if (index.IsOdd()) { coffeeItems[index].OfferCode = Constants.OfferCodeBOGO; }
            }
        }

        public void ApplyApplesPriceDropOffer(List<Item> purchasedItems)
        {
            // Find all apple items in the purchased items, exclude apples which already have APOM offer applied
            List<Item> appleItems = purchasedItems.FindAll(item => item.ItemType == Constants.ItemApple && item.OfferCode != Constants.OfferCodeAPOM);

            // Check if the apple items are 3 or more, then apply the price drop
            if (appleItems.Count >= 3)
            {
                Console.WriteLine("Applying price drop to $4.50 on Apples if you buy 3 or more bags.");
                // Apply the price drop to each apple item
                foreach (var appleItem in appleItems)
                {
                    // Update the price of each apple item to $4.50
                    appleItem.OfferCode = Constants.OfferCodeAPPL;
                }
            }
        }

        public void ApplyChaiMilkFreeOffer(List<Item> purchasedItems)
        {
            // Find the Chai item in the purchased items
            Item chaiItem = purchasedItems.Find(item => item.ItemType == Constants.ItemChai);

            // Check if the Chai item exists and there is no previous milk item
            if (chaiItem != null)
            {
                Console.WriteLine("Applying free milk with the purchase of a box of Chai (Limit 1).");
                if (purchasedItems.Exists(item => item.ItemType == Constants.ItemMilk))
                {
                    purchasedItems.FirstOrDefault(item => item.ItemType == Constants.ItemMilk).OfferCode = Constants.OfferCodeCHMK;
                }
                else
                {
                    Console.WriteLine("Adding free milk to the purchase.");
                    // Add a Milk item to the purchased items
                    Item milkItem = new Item { OfferCode = Constants.OfferCodeCHMK, ItemType = Constants.ItemMilk };
                    purchasedItems.Add(milkItem);
                }
            }
        }

        public void ApplyOatmealApplesDiscountOffer(List<Item> purchasedItems)
        {
            Console.WriteLine("Applying 50% off a bag of Apples with the purchase of a bag of Oatmeal.");

            // Find the Oatmeal and Apple items in the purchased items
            List<Item> oatmealItems = purchasedItems.FindAll(item => item.ItemType == Constants.ItemOatMeal);
            List<Item> appleItems = purchasedItems.FindAll(item => item.ItemType == Constants.ItemApple);

            // Check if both Oatmeal and Apple items exist
            if (oatmealItems.Count > 0 && appleItems.Count > 0)
            {
                //Apply 50% on the lesser number of items bewteen Oats and Apples
                List<Item> applyOfferOnAppleItemList = oatmealItems.Count < appleItems.Count ? appleItems.Take(oatmealItems.Count).ToList() : appleItems.Take(appleItems.Count).ToList();
                //Console.WriteLine("Applying 50% discount to the Apple item.");
                // Apply 50% discount to the Apple item
                applyOfferOnAppleItemList.ForEach(item => item.OfferCode = Constants.OfferCodeAPOM);
            }
        }

    }
}
