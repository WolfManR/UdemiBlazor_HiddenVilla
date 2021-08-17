using System;

namespace HiddenVilla_Client.ViewModels
{
    public class HomeVM
    {
        public DateTime StartData { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; } = 1;
    }
}
