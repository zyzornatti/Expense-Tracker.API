using AutoMapper;
using Expense_Tracker.API.CustomActionFilters;
using Expense_Tracker.API.CustomExceptions;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto categoryRequestDto)
        {
            var category = _mapper.Map<Category>(categoryRequestDto);
            category = await _categoryRepository.CreateAsync(category);
            return Ok(_mapper.Map<CategoryDto>(category));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");             
            }

            return Ok(_mapper.Map<CategoryDto>(existingCategory));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            var existingCategory = _mapper.Map<Category>(updateCategoryRequestDto);
            existingCategory = await _categoryRepository.UpdateAsync(id, existingCategory);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");                
            }

            return Ok(_mapper.Map<CategoryDto>(existingCategory));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.DeleteAsync(id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");                
            }

            return Ok(_mapper.Map<CategoryDto>(existingCategory));
        }

    }
}
