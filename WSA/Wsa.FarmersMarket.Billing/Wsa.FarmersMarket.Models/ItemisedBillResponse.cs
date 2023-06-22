using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsa.FarmersMarket.Models
{
    public class ItemisedBillResponse
    {
        public decimal TotalPrice {get; set; }
        public IList<Item> Items { get; set; }
    }
}
