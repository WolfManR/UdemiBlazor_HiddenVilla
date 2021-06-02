using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class HotelAmenity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Timing { get; set; }
        [Required]
        public string IconStyle { get; set; }
    }
}