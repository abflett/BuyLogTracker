using BuyLogTracker.Api.DTOs;
using BuyLogTracker.Api.Models;
using BuyLogTracker.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyLogTracker.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserPurchaseController : ControllerBase
    {
        private readonly UserPurchaseService _userPurchaseService;

        public UserPurchaseController(UserPurchaseService userPurchaseService)
        {
            _userPurchaseService = userPurchaseService;
        }

        [HttpPost("addUserPurchase")]
        public async Task<IActionResult> AddUserPurchase(UserPurchaseDTO userPurchase)
        {
            bool result = await _userPurchaseService.AddUserPurchase(userPurchase);

            if (result)
            {
                return Ok("User purchase added successfully.");
            }

            return BadRequest("Failed to add user purchase.");
        }

        [HttpPost("addPurchaseToUser")]
        public async Task<IActionResult> AddPurchaseToUser(PurchaseDTO purchase)
        {
            bool result = await _userPurchaseService.AddPurchaseToUser(purchase);

            if (result)
            {
                return Ok("Purchase added to user successfully.");
            }

            return BadRequest("Failed to add purchase to user.");
        }

        [HttpGet("findUsersByName")]
        public async Task<ActionResult<List<User>>> FindUsersByName(string searchString)
        {
            var users = await _userPurchaseService.FindUsersByName(searchString);
            return Ok(users);
        }

        [HttpGet("findUsersByPhone")]
        public async Task<ActionResult<List<User>>> FindUsersByPhone(string searchString)
        {
            var users = await _userPurchaseService.FindUsersByPhone(searchString);
            return Ok(users);
        }

        // Add other endpoints for FindUsersByEmail, FindUsersByPurchaseHistoryDescription, UserById, etc.

        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool result = await _userPurchaseService.DeleteUser(userId);

            if (result)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }

        // Add similar endpoints for UpdateUser, DeletePurchaseHistory, UpdatePurchaseHistory, etc.
    }
}
