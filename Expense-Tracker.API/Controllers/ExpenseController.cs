using AutoMapper;
using Expense_Tracker.API.CustomActionFilters;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var expenses = await expenseRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //Map the expenses into a Dto and return it
            return Ok(mapper.Map<List<ExpenseDto>>(expenses));
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

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateExpenseRequestDto updateExpenseRequestDto)
        {
            var existingExpense = mapper.Map<Expense>(updateExpenseRequestDto);
            existingExpense = await expenseRepository.UpdateAsync(id, existingExpense);

            if(existingExpense == null)
            {
                return NotFound("Expense with Id {id} not found.");                
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
                return NotFound("Expense with Id {id} not found.");                
            }

            return Ok(mapper.Map<ExpenseDto>(existingExpense));
        }
    }
}
