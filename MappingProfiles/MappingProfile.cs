using AutoMapper;
using BLL.AllDTOS;
using BLL.AllDTOS.CategoryDTOS;
using BLL.AllDTOS.DailyMenuDTOS;
using BLL.AllDTOS.DailyMenuFoodDTOS;
using BLL.AllDTOS.FoodCategoryDTOS;
using BLL.AllDTOS.FoodDTOS;
using BLL.AllDTOS.UserDTOS;
using DAL.Entities;
using KarYemek.Models;
using KarYemek.Models.CategoryViewModels;
using KarYemek.Models.DailyMenuFoodViewModels;
using KarYemek.Models.DailyMenuModels;
using KarYemek.Models.FoodCategoryViewModels;
using KarYemek.Models.FoodViewModels;

namespace KarYemek.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseDTO>().ReverseMap();
            CreateMap<BaseDTO,BaseViewModel>().ReverseMap();

            CreateMap<User, LoginDTO>().ReverseMap();

            //Category
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            //CategoryViewModel
            CreateMap< CategoryDTO,CategoryViewModel>().ReverseMap();
            CreateMap< CategoryDTO,GetCategoryViewModel>().ReverseMap();
            CreateMap< UpdateCategoryDTO,UpdateCategoryViewModel>().ReverseMap();
            CreateMap< CreateCategoryDTO,CreateCategoryViewModel>().ReverseMap();
            CreateMap< GetCategoryDTO,GetCategoryViewModel>().ReverseMap();
            CreateMap< GetCategoryDTO,UpdateCategoryViewModel>().ReverseMap();

            //Food
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<Food, CreateFoodDTO>().ReverseMap();
            CreateMap<Food, GetFoodDTO>().ReverseMap();
            CreateMap<Food, UpdateFoodDTO>().ReverseMap();
            //FoodViewModels
            CreateMap<UpdateFoodDTO,UpdateFoodViewModel>().ReverseMap();
            CreateMap<GetFoodDTO,UpdateFoodViewModel>().ReverseMap();
            CreateMap<GetFoodDTO,GetFoodViewModel>().ReverseMap();
            CreateMap<CreateFoodDTO,CreateFoodViewModel>().ReverseMap();
            CreateMap<FoodDTO,FoodViewModel>().ReverseMap();
            //DailyMenu
            CreateMap<DailyMenu,DailyMenuDTO>().ReverseMap();
            CreateMap<DailyMenu,GetDailyMenuDTO>().ReverseMap();
            CreateMap<DailyMenu,UpdateDailyMenuDTO>().ReverseMap();
            CreateMap<DailyMenu,CreateDailyMenuDTO>().ReverseMap();
            //DailyMenuViewModel
            CreateMap<DailyMenuDTO,DailyMenuViewModel>().ReverseMap();
            CreateMap<GetDailyMenuDTO,GetDailyMenuViewModel>().ReverseMap();
            CreateMap<UpdateDailyMenuDTO,UpdateDailyMenuViewModel>().ReverseMap();
            CreateMap<CreateDailyMenuDTO,CreateDailyMenuViewModel>().ReverseMap();
            //DailMenuFood
            CreateMap<DailyMenuFood,DailyMenuFoodDTO>().ReverseMap();
            CreateMap<DailyMenuFood,GetDailyMenuDTO>().ReverseMap();
     
            //DailMenuFoodViewModel
            CreateMap< DailyMenuFoodDTO,DailyMenuFoodViewModel>().ReverseMap();
            //FoodCategory
            CreateMap<FoodCategory, FoodCategoryDTO>().ReverseMap();
            //FoodCategoryViewModel
            CreateMap< FoodCategoryDTO,FoodCategoryViewModel>().ReverseMap();
            // FoodCategory -> FoodCategoryDTO
            CreateMap<FoodCategory, FoodCategoryDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.FoodId, opt => opt.MapFrom(src => src.FoodId));

            // FoodCategoryDTO -> FoodCategoryViewModel
            CreateMap<FoodCategoryDTO, FoodCategoryViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Food, opt => opt.MapFrom(src => src.Food));

            // Ters eşleme (DTO -> ViewModel ve ViewModel -> DTO)
            CreateMap<FoodCategoryDTO, FoodCategoryViewModel>().ReverseMap();




        }
    }
}
