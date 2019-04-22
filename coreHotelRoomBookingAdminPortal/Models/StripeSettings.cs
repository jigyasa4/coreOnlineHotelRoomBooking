using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreHotelRoomBookingAdminPortal.Models
{
    public class StripeSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
