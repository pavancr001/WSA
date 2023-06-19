using Microsoft.AspNetCore.Mvc;
using Wsa.Farmermarket.Services.Interfaces;
using Wsa.FarmersMarket.Models;

namespace Wsa.FarmersMarket.Billing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IRegister _register;
        public BillController(IRegister register)
        {
            _register = register;
        }

        [HttpPost(Name = "GetBill")]
        public Tuple<decimal, IList<Item>> Get([FromBody] List<string> purchasedItems)
        {
            return _register.CalculateTotalPrice(purchasedItems);
        }
    }
}
