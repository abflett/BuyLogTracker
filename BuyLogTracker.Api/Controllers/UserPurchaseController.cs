using BuyLogTracker.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyLogTracker.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserPurchaseController : ControllerBase
    {
        private readonly UserPurchaseService _userPurchase;

        public UserPurchaseController(UserPurchaseService userPurchaseService)
        {
            _userPurchase = userPurchaseService;
        }
    }
}
