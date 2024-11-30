using AutoMapper;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;

namespace Expense_Tracker.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();

            //Expense
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<AddExpenseRequestDto, Expense>().ReverseMap();
            CreateMap<UpdateExpenseRequestDto, Expense>().ReverseMap();

            //Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();

            //Budget
            CreateMap<Budget, BudgetDto>().ReverseMap();
            CreateMap<Budget, AddBudgetDto>().ReverseMap();
        }
    }
}
