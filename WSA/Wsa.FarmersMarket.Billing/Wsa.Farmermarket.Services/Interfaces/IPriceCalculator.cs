using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Interfaces
{
    public interface IPriceCalculator
    {
        Tuple<decimal, IList<Item>> CalculatePrice(List<Item> purchasedItems);
    }
}
