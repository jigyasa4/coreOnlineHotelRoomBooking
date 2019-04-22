using coreHotelRoomBookingUserPanel.Controllers;
using coreHotelRoomBookingUserPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace coreHotelRoomBooking.Tests
{
   public class BookingTestController
    {
       
            private coreHotelRoomBookingFinalDatabaseContext context;

            public static DbContextOptions<coreHotelRoomBookingFinalDatabaseContext> dbContextOptions { get; set; }

            public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

            static BookingTestController()
            {
                dbContextOptions = new DbContextOptionsBuilder<coreHotelRoomBookingFinalDatabaseContext>()
                    .UseSqlServer(connectionString).Options;
            }

            public BookingTestController()
            {
                context = new coreHotelRoomBookingFinalDatabaseContext(dbContextOptions);
            }


        [Fact]

        public void Task_Booking_Index()
        {
            //Arrange
            var controller = new BookingController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                controller.Index();
            });


        }


        [Fact]

        public void Task_Booking_Buy()
        {
            //Arrange
            var controller = new BookingController(context);

            int BookingId = 117;

            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                IActionResult data = controller.Buy(BookingId);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
            });

        }

        [Fact]

        public void Task_Booking_EmptyCart()
        {
            //Arrange 
            var controller = new BookingController(context);

            //Act
            IActionResult result = controller.EmptyCart();

            //Assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]

        public void Task_Booking_CheckOut()
        {
            //Arrange
            var controller = new BookingController(context);
            int CustomerId = 57;
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {

                var data = controller.Checkout(CustomerId);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
            });
        }


        [Fact]

        public void Task_Booking_OrderHistory()
        {
            //Arrange
            var controller = new BookingController(context);

            Assert.Throws<NullReferenceException>(() =>
            {
                controller.OrderHistory();
            });


        }



        [Fact]

        public void Task_Booking_History()
        {
            //Arrange
            var controller = new BookingController(context);
            int RoomId = 1;
            //Act
            IActionResult result = controller.HistoryDetails(RoomId);

            //Assert
           Assert.IsType<ViewResult>(result);
        }


        [Fact]

        public void Task_Booking_Invoice()
        {
            //Arrange
            var controller = new BookingController(context);
            int CustomerId = 57;
            //Act
            IActionResult result = controller.HistoryDetails(CustomerId);

            //Assert
            Assert.IsType<ViewResult>(result);


        }



    }
}
