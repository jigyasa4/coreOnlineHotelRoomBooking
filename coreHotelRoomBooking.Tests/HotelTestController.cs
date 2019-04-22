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
  public class HotelTestController
    {
        private HotelAdminDbContext context;

        public static DbContextOptions<HotelAdminDbContext> dbContextOptions { get; set; }

        public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static HotelTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<HotelAdminDbContext>()
                .UseSqlServer(connectionString).Options;
        }

        public HotelTestController()
        {
            context = new HotelAdminDbContext(dbContextOptions);
        }


        [Fact]
        public void Task_Get_All_Hotel_Return_OkResult()
        {
            //Arrange
            var controller = new HotelController(context);
            //Act
            var data = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Create_Hotel_Return_OkResult()
        {


            //Arrange
            var controller = new HotelController(context);

            //Act
            var data = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Add_Hotel_Return_OkResult()
        {//Arrange
            var controller = new HotelController(context);
            //Act
            var hotel = new Hotel()
            {
                HotelName = "Lila",
                HotelAddress = "Delhi",
                HotelDistrict = "Delhi",
                HotelCity = "Delhi",
                HotelState = "Delhi",
                HotelCountry = "India",
                HotelEmailId = "Lila@gmail.com",
                HotelRating = "1",
                HotelContactNumber = 123654789,
                HotelImage = "jkewhffj",
                HotelDescription = "Description",
                HotelTypeId = 2

            };
            var data = controller.Create(hotel);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);

        }

        //[Fact]
        //public void Task_DeleteHotelType_Return_View()
        //{
        //    //Arrange
        //    var controller = new HotelController(context);
        //    var id = 31;
        //    var ht = new Hotel()
        //    {
        //        HotelId = 31,


        //    };
        //    //Act
        //    var data = controller.Delete(id, ht);

        //    //Assert
        //    Assert.IsType<RedirectToActionResult>(data);

        //}

        [Fact]
        public void Task_Edit_Hotel_Return_View()
        {


            //Arrange
            var controller = new HotelController(context);
            int id = 32;


            var h = new Hotel()
            {
                HotelId = 32,
                HotelName = "Taj",
                HotelAddress = "21-Mohan Nagar",
                HotelDistrict = "South Delhi",
                HotelCity = "New Delhi",
                HotelState = "Delhi",
                HotelCountry = "India",
                HotelEmailId = "taj@gmail.com",
                HotelRating = "4",
                HotelContactNumber = 1213242,
                HotelImage = "https://media-cdn.tripadvisor.com/media/photo-s/14/f4/d4/24/oyo-9674-hotel-oberoi.jpg",
                HotelDescription = "Desc",
                HotelTypeId = 2


            };

            //Act

            var EditData = controller.Edit(id, h);

            //Assert
            Assert.IsType<RedirectToActionResult>(EditData);

        }

        [Fact]
        public void Task_Details_Hotel_Return_OkResult()
        {


            //Arrange
            var controller = new HotelRoomController(context);
            int HotelId = 5;
            //Act
            var data = controller.Details(HotelId);

            //Assert
            Assert.IsType<ViewResult>(data);

        }


    }
}
