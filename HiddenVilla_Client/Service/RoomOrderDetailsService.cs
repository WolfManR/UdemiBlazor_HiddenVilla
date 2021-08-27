using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient client;

        public RoomOrderDetailsService(HttpClient client)
        {
            this.client = client;
        }

        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }
    }
}
