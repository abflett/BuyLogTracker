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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            var result = await _userPurchase.CreateUser(user);

            if (result != null)
            {
                return Ok("User created successfully.");
            }

            return BadRequest("Failed to add new user.");
        }

        [HttpGet("Users")]
        public async Task<ActionResult<List<User>>> Users()
        {
            var users = await _userPurchase.Users();
            return Ok(users);
        }

        [HttpGet("PurchaseHistories")]
        public async Task<ActionResult<List<User>>> PurchaseHistories()
        {
            var users = await _userPurchase.PurchaseHistories();
            return Ok(users);
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

        [HttpGet("FindUsersByEmail")]
        public async Task<ActionResult<List<User>>> FindUsersByEmail(string searchString)
        {
            var users = await _userPurchase.FindUsersByEmail(searchString);
            return Ok(users);
        }

        [HttpGet("FindUsersByPurchaseHistoryDescription")]
        public async Task<ActionResult<List<User>>> FindUsersByPurchaseHistoryDescription(string searchString)
        {
            var users = await _userPurchase.FindUsersByPurchaseHistoryDescription(searchString);
            return Ok(users);
        }

        [HttpGet("UserById/{id}")]
        public async Task<ActionResult<List<User>>> UserById(int id)
        {
            var user = await _userPurchase.UserById(id);
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User updatedUser)
        {
            bool result = await _userPurchase.UpdateUser(updatedUser);
            if (result)
            {
                return Ok("User updated successfully.");
            }
            else
            {
                return NotFound("User not found or update failed.");
            }
        }

        [HttpPut("UpdatePurchaseHistory")]
        public async Task<IActionResult> UpdatePurchaseHistory(PurchaseHistory updatedPurchaseHistory)
        {
            bool result = await _userPurchase.UpdatePurchaseHistory(updatedPurchaseHistory);
            if (result)
            {
                return Ok("Purchase history updated successfully.");
            }
            else
            {
                return NotFound("Purchase history not found or update failed.");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool result = await _userPurchase.DeleteUser(id);

            if (result)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }

        [HttpDelete("DeletePurchaseHistory/{id}")]
        public async Task<IActionResult> DeletePurchaseHistory(int id)
        {
            bool result = await _userPurchase.DeletePurchaseHistory(id);

            if (result)
            {
                return Ok("PurchaseHistory deleted successfully.");
            }

            return BadRequest("Failed to delete PurchaseHistory.");
        }
    }
}
