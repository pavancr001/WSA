using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Interfaces
{
    public interface IOfferProcessor
    {
        bool IsOfferApplicable(string offerCode, List<Item> purchasedItems);
        void ApplyOffer(string offerCode, List<Item> purchasedItems);
        void ApplyBuyOneGetOneFreeCoffeeOffer(List<Item> purchasedItems);
        void ApplyApplesPriceDropOffer(List<Item> purchasedItems);
        void ApplyChaiMilkFreeOffer(List<Item> purchasedItems);

        void ApplyOatmealApplesDiscountOffer(List<Item> purchasedItems);
    }
}
