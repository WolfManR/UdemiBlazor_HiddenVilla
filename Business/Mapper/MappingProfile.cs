using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>().ReverseMap();
            CreateMap<HotelRoomImageDTO, HotelRoomImage>().ReverseMap();
            CreateMap<HotelAmenityDTO, HotelAmenity>().ReverseMap();

            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>()
                .ForMember(x => x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
        }
    }
}