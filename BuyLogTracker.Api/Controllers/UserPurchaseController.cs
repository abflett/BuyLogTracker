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
        private readonly UserPurchaseService _userPurchase;

        public UserPurchaseController(UserPurchaseService userPurchaseService)
        {
            _userPurchase = userPurchaseService;
        }

        [HttpPost("AddUserPurchase")]
        public async Task<IActionResult> AddUserPurchase(UserPurchaseDTO userPurchase)
        {
            bool result = await _userPurchase.AddUserPurchase(userPurchase);

            if (result)
            {
                return Ok("User purchase added successfully.");
            }

            return BadRequest("Failed to add user purchase.");
        }

        [HttpPost("AddPurchaseToUser")]
        public async Task<IActionResult> AddPurchaseToUser(PurchaseDTO purchase)
        {
            bool result = await _userPurchase.AddPurchaseToUser(purchase);

            if (result)
            {
                return Ok("Purchase added to user successfully.");
            }

            return BadRequest("Failed to add purchase to user.");
        }

        [HttpGet("FindUsersByName")]
        public async Task<ActionResult<List<User>>> FindUsersByName(string searchString)
        {
            var users = await _userPurchase.FindUsersByName(searchString);
            return Ok(users);
        }

        [HttpGet("FindUsersByPhone")]
        public async Task<ActionResult<List<User>>> FindUsersByPhone(string searchString)
        {
            var users = await _userPurchase.FindUsersByPhone(searchString);
            return Ok(users);
        }

        [HttpGet("UserById/{id}")]
        public async Task<ActionResult<List<User>>> UserById(int id)
        {
            var user = await _userPurchase.UserById(id);
            return Ok(user);
        }
        // Add other endpoints for FindUsersByEmail, FindUsersByPurchaseHistoryDescription, UserById, etc.

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool result = await _userPurchase.DeleteUser(userId);

            if (result)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }

        // Add similar endpoints for UpdateUser, DeletePurchaseHistory, UpdatePurchaseHistory, etc.
    }
}
