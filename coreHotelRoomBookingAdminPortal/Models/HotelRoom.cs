using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreHotelRoomBookingAdminPortal.Models
{
    public class HotelRoom
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int RoomPrice { get; set; }
        [Required]
        public string RoomDescription { get; set; }
        [Required]
        public string RoomImage { get; set; }

        [Required]
        public int HotelId { get; set; }

        //Navigation Properties

        public RoomFacility RoomFacility { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
