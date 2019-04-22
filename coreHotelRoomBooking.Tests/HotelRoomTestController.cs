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
    public class HotelRoomTestController
    {
        private HotelAdminDbContext context;

        public static DbContextOptions<HotelAdminDbContext> dbContextOptions { get; set; }

        public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static HotelRoomTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<HotelAdminDbContext>()
                .UseSqlServer(connectionString).Options;
        }

        public HotelRoomTestController()
        {
            context = new HotelAdminDbContext(dbContextOptions);
        }

        [Fact]
        public void Task_Get_All_HotelRoom_Return_OkResult()
        {
            //Arrange
            var controller = new HotelRoomController(context);
            //Act
            var data = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(data);

        }


        [Fact]
        public void Task_Create_HotelRoom_Return_OkResult()
        {


            //Arrange
            var controller = new HotelRoomController(context);

            //Act
            var data = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(data);

        }


        [Fact]
        public void Task_Details_HotelRoom_Return_OkResult()
        {


            //Arrange
            var controller = new HotelRoomController(context);
            int RoomId = 5;
            //Act
            var data = controller.Details(RoomId);

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Add_Hotel_Return_OkResult()
        {//Arrange
            var controller = new HotelRoomController(context);
            //Act
            var hotel = new HotelRoom()
            {
                RoomType = "Delux",
                RoomPrice = 2500,
                RoomDescription = "Delux Description",
                RoomImage = "https://media-cdn.tripadvisor.com/media/photo-s/14/f4/d4/24/oyo-9674-hotel-oberoi.jpg",
                HotelId = 2

            };
            var data = controller.Create(hotel);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);

        }

        [Fact]
        public void Task_Edit_Hotelroom_Return_View()
        {


            //Arrange
            var controller = new HotelRoomController(context);
            int id = 14;

            var h = new HotelRoom()
            {
                RoomType = "Delux1",
                RoomPrice = 2500,
                RoomDescription = "Delux Description",
                RoomImage = "https://media-cdn.tripadvisor.com/media/photo-s/14/f4/d4/24/oyo-9674-hotel-oberoi.jpg",
                HotelId = 2

            };

            //Act

            var EditData = controller.Edit(id, h);

            //Assert
            Assert.IsType<RedirectToActionResult>(EditData);
        }


        //[Fact]
        //public void Task_DeleteHotelRoom_Return_View()
        //{
        //    //Arrange
        //    var controller = new HotelRoomController(context);
        //    var id = 13;
        //    var ht = new HotelRoom()
        //    {
        //        RoomId = 13,


        //    };
        //    //Act
        //    var data = controller.Delete(id, ht);

        //    //Assert
        //    Assert.IsType<RedirectToActionResult>(data);

        //}

    }
}
