using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Interfaces
{
    public interface IMapper
    {
        ItemisedBillResponse Map(Tuple<decimal, IList<Item>> tuple);
    }
}
