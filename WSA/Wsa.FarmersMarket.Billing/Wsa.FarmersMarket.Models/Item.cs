using Wsa.FarmersMarket.Core;

namespace Wsa.FarmersMarket.Models
{
    public class Item
    {        
        public string OfferCode { get; set; }
        public string ItemType { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}