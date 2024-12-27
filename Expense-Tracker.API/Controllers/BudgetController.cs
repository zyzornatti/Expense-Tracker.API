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

namespace Expense_Tracker.API.Controllers
{
    [Route("api/budgets")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetRepository;
        private readonly IMapper _mapper;

        public BudgetController(IBudgetService budgetRepository, IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBudget([FromBody] AddBudgetDto budgetDto)
        {
            var budget =  _mapper.Map<Budget>(budgetDto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new ResourceNotFoundException("User not authenticated.");
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

            await _budgetRepository.CreateBudgetAsync(budget);

            return Ok(new { Message = "Budget created successfully." });
            //return Ok(_mapper.Map<BudgetDto>(budget));
        }

        [HttpGet("reports/budget-insights")]
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


            var insights = await _budgetRepository.GetBudgetInsights(parsedUserId, startDate, endDate, categoryId);

            return Ok(insights);
        }
    }
}
