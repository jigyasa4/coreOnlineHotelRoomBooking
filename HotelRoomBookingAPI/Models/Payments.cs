﻿using System;
using System.Collections.Generic;

namespace HotelRoomBookingAPI.Models
{
    public partial class Payments
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentDescription { get; set; }
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
        public int PaymentCardNumber { get; set; }
        public string StripePaymentId { get; set; }

        public Bookings Booking { get; set; }
    }
}
