using AutoMapper;
using Expense_Tracker.API.CustomActionFilters;
using Expense_Tracker.API.CustomExceptions;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expense_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;
        //private readonly ExpenseTrackerDbContext dbContext;

        public ExpenseController(IExpenseRepository expenseRepository, IMapper mapper)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
            //this.dbContext = dbContext;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddExpenseRequestDto expenseRequestDto)
        {
            var expense = mapper.Map<Expense>(expenseRequestDto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new ResourceNotFoundException("User not authenticated.");
            }

            expense = new Expense
            {
                UserId = parsedUserId,
                CategoryId = expense.CategoryId,
                Amount = expense.Amount,
                Description = null ?? expense.Description,
                Date = expense.Date,
            };

            expense = await expenseRepository.CreateAsync(expense);

            return Ok(mapper.Map<ExpenseDto>(expense));
        }

        // GET: /api/expense?filterOn=Name&filteryQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=1000
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //return await dbContext.Expenses.ToListAsync();
            //Get all expenses from the database
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                var expenses = await expenseRepository.GetAllAsync(userId, filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
                //Map the expenses into a Dto and return it  
                return Ok(mapper.Map<List<ExpenseDto>>(expenses));
            }

            throw new ResourceNotFoundException("User Not Authenticated");
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingExpense = await expenseRepository.GetByIdAsync(id);
            if(existingExpense == null)
            {
                return NotFound("Expense with Id {id} not found.");
            }

            //Map the expenses into a Dto and return it
            return Ok(mapper.Map<ExpenseDto>(existingExpense));
        }

        [HttpGet("total")]
        //[Authorize]
        public async Task<IActionResult> GetTotalExpenses([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //return Ok(userIdClaim);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new ResourceNotFoundException("User not authenticated.");
            }

            if (startDate == default || endDate == default)
            {
                return BadRequest("Start date and end date are required.");
            }

            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            try
            {
                var totalExpenses = await expenseRepository.GetTotalExpensesAsync(userId, startDate, endDate);
                return Ok(new
                {
                    Total = totalExpenses,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateExpenseRequestDto updateExpenseRequestDto)
        {
            var existingExpense = mapper.Map<Expense>(updateExpenseRequestDto);
            existingExpense = await expenseRepository.UpdateAsync(id, existingExpense);

            if(existingExpense == null)
            {
                return NotFound($"Expense with Id {id} not found.");                
            }

            return Ok(mapper.Map<ExpenseDto>(existingExpense));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var existingExpense = await expenseRepository.DeleteAsync(id);

            if (existingExpense == null)
            {
                return NotFound($"Expense with Id {id} not found.");                
            }

            return Ok(mapper.Map<ExpenseDto>(existingExpense));
        }
    }
}
