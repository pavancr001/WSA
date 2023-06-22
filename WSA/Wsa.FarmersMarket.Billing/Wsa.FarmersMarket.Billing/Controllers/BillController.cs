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
        private readonly IMapper _mapper;
        public BillController(IRegister register, IMapper mapper)
        {
            _register = register;
            _mapper = mapper;
        }

        [HttpPost(Name = "GetBill")]
        public ItemisedBillResponse Get([FromBody] List<string> purchasedItems)
        {
            var calculatedItems = _register.CalculateTotalPrice(purchasedItems);
            return _mapper.Map(calculatedItems);
        }
    }
}
