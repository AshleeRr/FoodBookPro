using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Application.Mapping
{
    public class BookingProfile: Profile
    {
        public BookingProfile()
        {
            #region Booking
            CreateMap<Booking, SaveBookingViewModel>()
                .ReverseMap()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Restaurant, opt => opt.Ignore())
                .ForMember(x => x.Table, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());

            /*CreateMap<Booking, BookingViewModel>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
                .ForMember(x => x.RestaurantName, opt => opt.MapFrom(x => x.Restaurant.Name))
                .ForMember(x => x.TableNumber, opt => opt.MapFrom(x => x.Table.TableNumber))
                .ReverseMap();*/
            #endregion
            

        }
    }
}
