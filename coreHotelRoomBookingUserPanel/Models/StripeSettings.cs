using System;
using System.Collections.Generic;

namespace coreHotelRoomBookingUserPanel.Models
{
    public partial class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
