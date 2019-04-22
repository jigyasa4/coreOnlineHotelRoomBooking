using coreHotelRoomBookingAdminPortal.Controllers;
using coreHotelRoomBookingAdminPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace coreHotelRoomBooking.Tests
{
  public  class RoomFacilityTestController
    {
        private HotelAdminDbContext context;

        public static DbContextOptions<HotelAdminDbContext> dbContextOptions { get; set; }

        public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static RoomFacilityTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<HotelAdminDbContext>()
                .UseSqlServer(connectionString).Options;
        }

        public RoomFacilityTestController()
        {
            context = new HotelAdminDbContext(dbContextOptions);
        }

        [Fact]
        public void Task_Get_All_RoomFacility_Return_OkResult()
        {
            //Arrange
            var controller = new RoomFacilityController(context);
            //Act
            var data = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Create_RoomFacility_Return_OkResult()
        {


            //Arrange
            var controller = new RoomFacilityController(context);

            //Act
            var data = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Details_RoomFacility_Return_OkResult()
        {


            //Arrange
            var controller = new RoomFacilityController(context);
            int RoomFacilityId = 2;
            //Act
            var data = controller.Details(RoomFacilityId);

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]

        public async void Task_AddRoomFacility_Return_OkResult()
        {//Arrange
            var controller = new RoomFacilityController(context);
            var room = new RoomFacility()
            {

                IsAvilable = true,
                Wifi = true,
                AirConditioner = true,
                Refrigerator = true,
                RoomFacilityDescription = "Good",
                RoomId = 14


            };
            //Act
            var data = controller.Create(room);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);

        }

        [Fact]

        public async void Task_EditRoomFacility_Return_OkResult()
        {//Arrange
            var controller = new RoomFacilityController(context);
            int id = 9;
            var room = new RoomFacility()
            {

                IsAvilable = false,
                Wifi = true,
                AirConditioner = true,
                Refrigerator = true,
                RoomFacilityDescription = "Good",
                RoomId = 14


            };
            //Act
            var data = controller.Edit(id, room);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);

        }

        //[Fact]
        //public void Task_DeleteRoomFacility_Return_View()
        //{
        //    //Arrange
        //    var controller = new RoomFacilityController(context);
        //    var id = 9;
        //    var ht = new RoomFacility()
        //    {
        //        RoomFacilityId = 9,


        //    };
        //    //Act
        //    var data = controller.Delete(id, ht);

        //    //Assert
        //    Assert.IsType<RedirectToActionResult>(data);

        //}




    }
}
