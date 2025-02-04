using AutoMapper;
using Expense_Tracker.API.CustomExceptions;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Expense_Tracker.API.Controllers
{
    [Route("api/budgets")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly IMapper _mapper;

        public BudgetController(IBudgetService budgetService, IMapper mapper)
        {
            _budgetService = budgetService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] AddBudgetDto budgetDto)
        {
            var budget =  _mapper.Map<Budget>(budgetDto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest("User not authenticated.");
            }

            if (budgetDto.StartDate >= budget.EndDate)
            {
                return BadRequest("StartDate must be earlier than EndDate.");
            }

            budget = new Budget
                {
                    UserId = parsedUserId,
                    CategoryId = budget.CategoryId,
                    Amount = budget.Amount,
                    StartDate = budget.StartDate,
                    EndDate = budget.EndDate
                };

            await _budgetService.CreateBudgetAsync(budget);

            return Ok(new { Message = "Budget created successfully." });
            //return Ok(_mapper.Map<BudgetDto>(budget));
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetBudgets(Guid userId)
        {
            var budgets = await _budgetService.GetAllBudgetAsync(userId);
            if (budgets == null)
            {
                return NotFound("Budgets for this user {id} not found.");
            }

            return Ok(_mapper.Map<List<BudgetDto>>(budgets));
        }


        [HttpGet("insights")]
        public async Task<IActionResult> BudgetInsights([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] Guid? categoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new ResourceNotFoundException("User not authenticated.");
            }

            if (startDate >= endDate)
            {
                return BadRequest("StartDate must be earlier than EndDate.");
            }


            var insights = await _budgetService.GetBudgetInsights(parsedUserId, startDate, endDate, categoryId);

            return Ok(insights);
        }
    }
}
