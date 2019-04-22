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
   public  class HomeTestController
    {

        private coreHotelRoomBookingFinalDatabaseContext context;

        public static DbContextOptions<coreHotelRoomBookingFinalDatabaseContext> dbContextOptions { get; set; }

        public static string connectionString =
        "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static HomeTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<coreHotelRoomBookingFinalDatabaseContext>()
                .UseSqlServer(connectionString).Options;
        }

        public HomeTestController()
        {
            context = new coreHotelRoomBookingFinalDatabaseContext(dbContextOptions);
        }

        [Fact]
        public void Task_GetDetails_Hotel()
        {
            //Arrange
            var controller = new HomeController(context);
            int HotelId = 1;

            //Act
            IActionResult data = controller.Details(HotelId);
            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_GetDetails_HotelType()
        {
            //Arrange
            var controller = new HomeController(context);
            int HotelTypeId = 1;

            //Act
            IActionResult data = controller.Details(HotelTypeId);
            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_GetRoomDetailsFound()
        {//Arrange
            var controller = new HomeController(context);
            var RoomId = 1;

            //Act
            IActionResult data = controller.Details(RoomId);
            //Assert
            Assert.IsType<ViewResult>(data);


        }


        [Fact]
        public void Task_GetDetails_IndexSession()
        {
            //Arrange
            var controller = new HomeController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                var data = controller.Index();
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
            });
        }
        [Fact]

        public void Task_HotelRoomIndex()
        {
            //Arrange
            var controller = new HomeController(context);
            //Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                controller.HotelRoomsIndex(1);
            });
        }

        [Fact]

        public void Task_Get_RoomDetails()
        {
            //Arrange
            var controller = new HomeController(context);
            var RoomId = 1;
            Assert.Throws<NullReferenceException>(() =>
            {
                //Act
                IActionResult data = controller.RoomsDetails(RoomId);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
            });
        }

        [Fact]

        public void Task_Get_RoomDetails1()
        {
            //Arrange
            var controller = new HomeController(context);
            var RoomFacilityId = 1;
            Assert.Throws<NullReferenceException>(() =>
            {
                //Act
                IActionResult data = controller.RoomsDetails(RoomFacilityId);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
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



    }
}
