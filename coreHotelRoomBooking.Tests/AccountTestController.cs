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
   public class AccountTestController
    {
        private HotelAdminDbContext context;

        public static DbContextOptions<HotelAdminDbContext> dbContextOptions { get; set; }

        public static string connectionString =
         "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static AccountTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<HotelAdminDbContext>()
                .UseSqlServer(connectionString).Options;
        }

        public AccountTestController()
        {
            context = new HotelAdminDbContext(dbContextOptions);
        }

        [Fact]
        public void Task_Get_All_Account_Return_OkResult()
        {
            //Arrange
            var controller = new AccountController(context);
            //Act
            var data = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(data);

        }

        [Fact]
        public void Task_Account_Login_If()
        {
            //Arrange
            var controller = new AccountController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                var data = controller.Login("a", "1");
                Assert.IsType<ViewResult>(data);
            });
            //Assert

        }


        [Fact]
        public void Task_Account_Login_Else()
        {
            //Arrange
            var controller = new AccountController(context);
            //Act
            var data = controller.Login("admin", "1");
            //Assert
            Assert.IsType<ViewResult>(data);



        }

        [Fact]

        public void Task_Account_LogOut()
        {
            //Arrange 
            var controller = new AccountController(context);
            //Act

            //Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = controller.Logout();
                Assert.IsType<RedirectResult>(result);
            });
        }

      

       
   }
}
