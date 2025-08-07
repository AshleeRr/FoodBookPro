using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Application.Mapping
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            #region User
            //User -> SaveUserViewModel
            CreateMap<User, SaveUserViewModel>()
                .ForMember(x => x.UserTypes, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Restaurant, opt => opt.Ignore())
                .ForMember(x => x.UserType, opt => opt.Ignore())
                .ForMember(x => x.Bookings, opt => opt.Ignore())
                .ForMember(x => x.Orders, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ForMember(x => x.Notifications, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore()); // Ignore Id for SaveUserViewModel to prevent overwriting
                
            //User -> UserViewModel
            CreateMap<User, UserViewModel>()
                .ReverseMap();

            //User -> LoginViewModel
            CreateMap<User, LoginViewModel>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.Ignore())
                .ForMember(x => x.LastName, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.UserType, opt => opt.Ignore())
                .ForMember(x => x.Restaurant, opt => opt.Ignore())
                .ForMember(x => x.Bookings, opt => opt.Ignore())
                .ForMember(x => x.Orders, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ForMember(x => x.Notifications, opt => opt.Ignore());

            #endregion

        }
    }
}
