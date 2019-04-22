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
  public  class HotelTypeTestController
    {
        private HotelAdminDbContext context;

        public static DbContextOptions<HotelAdminDbContext> dbContextOptions { get; set; }

        public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static HotelTypeTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<HotelAdminDbContext>()
                .UseSqlServer(connectionString).Options;
        }

        public HotelTypeTestController()
        {
            context = new HotelAdminDbContext(dbContextOptions);
        }
        [Fact]
        public void Task_Get_All_HotelType_Return_OkResult()
        {
            //Arrange
            var controller = new HTypeController(context);
            //Act
            var data = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(data);

        }


        [Fact]
        public void Task_Create_HotelType_Return_OkResult()
        {


            //Arrange
            var controller = new HTypeController(context);

            //Act
            var data = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Add_HotelType_Return_OkResult()
        {//Arrange
            var controller = new HTypeController(context);
            //Act
            var Htype = new HotelType()
            {
                HotelTypeName = "Motel",
                HotelTypeDescription = "Good"

            };
            var data = controller.Create(Htype);
            //Assert
            Assert.IsType<RedirectToActionResult>(data);

        }

        [Fact]
        public void Task_Details_HotelType_Return_OkResult()
        {


            //Arrange
            var controller = new HTypeController(context);
            int HotelTypeId = 5;
            //Act
            var data = controller.Details(HotelTypeId);

            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Edit_HotelType_Return_View()
        {
            //Arrange
            var controller = new HTypeController(context);
            int id = 13;


            var ht = new HotelType()
            {
                HotelTypeId = 13,
                HotelTypeName = "Villa!",
                HotelTypeDescription = "Good!"

            };

            //Act

            var EditData = controller.Edit(id, ht);

            //Assert
            Assert.IsType<RedirectToActionResult>(EditData);

        }

        //[Fact]
        //public void Task_DeleteHotelType_Return_View()
        //{
        //    //Arrange
        //    var controller = new HTypeController(context);
        //    var id = 12;
        //    var ht = new HotelType()
        //    {
        //        HotelTypeId = 12,
             

        //    };
        //    //Act
        //    var data = controller.Delete(id, ht);

        //    //Assert
        //    Assert.IsType<RedirectToActionResult>(data);

        //}


    }
}
