using Models;

using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IRoomOrderDetailsService
    {
        Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details);
        Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details);
    }
}
