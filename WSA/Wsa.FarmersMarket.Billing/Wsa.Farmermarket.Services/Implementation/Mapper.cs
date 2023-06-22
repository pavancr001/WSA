using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsa.Farmermarket.Services.Interfaces;
using Wsa.FarmersMarket.Models;

namespace Wsa.Farmermarket.Services.Implementation
{
    public class Mapper : IMapper
    {
        public ItemisedBillResponse Map(Tuple<decimal, IList<Item>> tuple)
        {
            return new ItemisedBillResponse
            {
                TotalPrice = tuple.Item1,
                Items = tuple.Item2
            };
        }
    }
}
