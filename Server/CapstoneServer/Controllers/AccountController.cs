using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CapstoneServer.DTOs;
using CapstoneServer.Services;

namespace CapstoneServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // Create Account
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateAccount(string userId, [FromBody] CreateAccountDTO createAccountDTO)
        {
            try
            {
                var accountDTO = await _accountService.CreateAccountAsync(userId, createAccountDTO);
                return Ok(accountDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Account by ID
        [HttpGet("{accNum}")]
        public async Task<IActionResult> GetAccountByAccountNumber(string accNum)
        {
            try
            {
                var account = await _accountService.GetAccountByAccountNumberAsync(accNum);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Update Account Type
        [HttpPatch("{accNum}/type")]
        public async Task<IActionResult> UpdateAccountType(string accNum, [FromBody] UpdateAccountDTO updateAccountDTO)
        {
            try
            {
                await _accountService.UpdateAccountTypeAsync(accNum, updateAccountDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Account Balance
        [HttpPatch("{accNum}/balance")]
        public async Task<IActionResult> UpdateAccountBalance(string accNum, [FromBody] UpdateAccountDTO updateAccountDTO)
        {
            try
            {
                if(updateAccountDTO.Amount == null){
					Exception ex = new Exception("Amount field is empty");
					return BadRequest(ex.Message);
				}
				await _accountService.UpdateBalanceAsync(accNum, updateAccountDTO.Amount.Value);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Activate Account
        [HttpPatch("{accNum}/activate")]
        public async Task<IActionResult> ActivateAccount(string accNum)
        {
            try
            {
                await _accountService.ActivateAccountAsync(accNum);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deactivate Account
        [HttpPatch("{accNum}/deactivate")]
        public async Task<IActionResult> DeactivateAccount(string accNum)
        {
            try
            {
                await _accountService.DeactivateAccountAsync(accNum);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
