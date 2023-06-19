using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Interfaces
{
    public interface IRegister
    {
        Tuple<decimal,IList<Item>> CalculateTotalPrice(List<string> purchasedItems);
    }
}
