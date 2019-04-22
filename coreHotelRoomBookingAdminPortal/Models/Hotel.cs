using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreHotelRoomBookingAdminPortal.Models
{
    public class Hotel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string HotelAddress { get; set; }
        [Required]
        public string HotelDistrict { get; set; }
        [Required]
        public string HotelCity { get; set; }
        [Required]

        public string HotelState { get; set; }
        [Required]

        public string HotelCountry { get; set; }
        [Required]
        public string HotelEmailId { get; set; }
        [Required]
        public string HotelRating { get; set; }
        [Required]
        public long HotelContactNumber { get; set; }
        [Required]
        public string HotelImage { get; set; }
        [Required]
        public string HotelDescription { get; set; }


        public int HotelTypeId { get; set; }


        public virtual HotelType HotelType { get; set; }
        public virtual List<HotelRoom> HotelRooms { get; set; }
        //public List<HotelRoom> HotelRooms { get; set; }

        //public HotelType HotelType { get; set; }
    }
}
