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
    public class Register : IRegister
    {
        private readonly IOfferProcessor _offerProcessor;
        private readonly IPriceCalculator _priceCalculator;
        public Register(IOfferProcessor offerProcessor, IPriceCalculator priceCalculator)
        {
            _offerProcessor = offerProcessor;
            _priceCalculator = priceCalculator;
        }

        public Tuple<decimal, IList<Item>> CalculateTotalPrice(List<string> purchasedItems)
        {
            var items = purchasedItems.Select(purchasedItem => new Item { ItemType = purchasedItem }).ToList();
            foreach (string offer in Enum.GetNames(typeof(Enums.Offers)))            
                _offerProcessor.ApplyOffer(offer, items);
            return _priceCalculator.CalculatePrice(items);
        }
    }
}
