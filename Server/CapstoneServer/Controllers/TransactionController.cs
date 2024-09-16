using CapstoneServer.DTOs;
using CapstoneServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneServer.Repositories;

namespace CapstoneServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
	private readonly TransactionService _transactionService;

	public TransactionController(TransactionService transactionService)
	{
		_transactionService = transactionService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO createTransactionDTO)
	{
		try
		{
			await _transactionService.CreateTransactionAsync(createTransactionDTO);
			return Ok("Transaction created successfully.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("account/{accountNumber}")]
	public async Task<IActionResult> GetTransactionsByAccountNumber(string accountNumber)
	{
		try
		{
			var transactions = await _transactionService.GetTransactionsByAccountNumberAsync(accountNumber);
			return Ok(transactions);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetTransactionsByUserId(string userId)
	{
		try
		{
			var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
			return Ok(transactions);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
