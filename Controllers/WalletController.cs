using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeoPay.Entities;
using NeoPay.Services;
using System.Net;
using System.Security.Claims;

namespace NeoPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly APIResponse _response;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetWallets()
        {
          
            var walletList = await _walletService.GetAllWalletAsync();
                _response.IsSuccess = true;
                _response.Result = walletList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(walletList);
        }

        [HttpGet("my-wallets")]
        [Authorize]
        public async Task<IActionResult> GetMyWallets()
        {
 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add("Invalid or missing token.");
                return Unauthorized(_response);
            }

            var wallets = await _walletService.GetUserWalletsAsync(userId);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = wallets;

            return Ok(_response);
        }

        [HttpGet("{walletId}")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetWalletById(Guid walletId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add("Invalid or missing token.");
                return Unauthorized(_response);
            }
            var walllets = await _walletService.GetWalletsByIdAsync(walletId);
               _response.IsSuccess = true;
               _response.StatusCode = HttpStatusCode.OK;
               _response.Result = walllets;
                return Ok(_response);
        }

        [HttpGet("currency/{currency}")]
        public async Task<ActionResult<APIResponse>> GetWalletByCurrency(string userId, string currency)
        {
            var userWallet = await _walletService.GetWalletByCurrencyAsync(userId, currency);
                  if (userWallet == null)
                  {
                      return NotFound(_response);
                  }
                  _response.IsSuccess = true;
                  _response.StatusCode = HttpStatusCode.OK;
                   _response.Result = userWallet;
                   return Ok(_response);
        }
    }
}
