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
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ReverseMap();
        }
    }
}