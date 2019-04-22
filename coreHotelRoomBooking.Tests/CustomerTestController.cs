using coreHotelRoomBookingUserPanel.Controllers;
using coreHotelRoomBookingUserPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace coreHotelRoomBooking.Tests
{
    public class CustomerTestController
    {
        private coreHotelRoomBookingFinalDatabaseContext context;

        public static DbContextOptions<coreHotelRoomBookingFinalDatabaseContext> dbContextOptions { get; set; }

        public static string connectionString =
        "Data Source=TRD-519;Initial Catalog=coreHotelRoomBookingFinalDatabase;Integrated Security=true;";

        static CustomerTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<coreHotelRoomBookingFinalDatabaseContext>()
                .UseSqlServer(connectionString).Options;
        }

        public CustomerTestController()
        {
            context = new coreHotelRoomBookingFinalDatabaseContext(dbContextOptions);
        }
        [Fact]

        public void Task_Customer_Index()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                var data = controller.Index();
            //Assert
            Assert.IsType<RedirectToActionResult>(data);
            });
        }

        //[Fact]
        //public void Task_Add_Customer_Return_OkResult()
        //{//Arrange
        //    var controller = new CustomerController(context);
        //    //Act
        //    var customer = new Customers()
        //    {
        //        CustomerFirstName = "Aditi",
        //        CustomerLastName = "Gupta",
        //        CustomerAddress = "21-A",
        //        CustomerContactNumber = 997321212,
        //        CustomerEmailId = "aditi@gmail.com",
        //        CustomerPassword = "1",
        //        State = "New Delhi",
        //        Country = "India",
        //        Zip = 110034
        //    };
        //    var data = controller.Create(customer);
        //    //Assert
        //    Assert.IsType<RedirectToActionResult>(data);

        //}
        [Fact]

        public void Task_Customer_Login_If()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
                var CustomerFirstName = "Mohit";
                var CustomerPassword = "12345";
                var data = controller.MyLogin(CustomerFirstName, CustomerPassword);

            //Assert
                Assert.IsType<RedirectToActionResult>(data);
          
        }

        [Fact]

        public void Task_Customer_Login_Else()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
            IActionResult result = controller.MyLogin("Kartik", "123456");
            ViewResult view = (ViewResult)result;
            string test = "Invalid Credentials";
            string data = (string)view.ViewData["Error"];

            //Assert
            Assert.Equal(test, data);

        }

        [Fact]

        public void Task_Customer_LogOut()
        {
            //Arrange 
            var controller = new CustomerController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = controller.Logout();
            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            });
        }

        [Fact]

        public void Task_Customer_Details()
        {
            //Arrange
            var controller = new CustomerController(context);
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = controller.Details();
                Assert.IsType<ViewResult>(result);
            });
        }

        [Fact]

        public void Task_Customer_Edit()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
            Assert.Throws<NullReferenceException>(() =>
            {
                var result = controller.Edit();
            //Arrange
                Assert.IsType<RedirectToActionResult>(result);
            });
        }
        //[Fact]

        //public void Task_Customer_Edit1()
        //{//Arrange
        //    var controller = new CustomerController(context);
        //    int CustomerId = 59; 
        //    var customer = new Customers()
        //        {
        //            CustomerId = 59,
        //            CustomerFirstName = "Adi",
        //            CustomerLastName = "Gupta",
        //            CustomerAddress = "21-A",
        //            CustomerContactNumber = 9973212121,
        //            CustomerEmailId = "aditi@gmail.com",
        //            CustomerPassword = "1",
        //            State = "New Delhi",
        //            Country = "India",
        //            Zip = 110095,
                    
        //        };
        //    //Act
        //        var data = controller.Edit(customer);
        //    //Assert
        //        Assert.IsType<RedirectToActionResult>(data);
          
        //}


        [Fact]

        public void Task_Customer_ChangePassword()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
            var result = controller.ChangePassword();
            //Arrange
            Assert.IsType<ViewResult>(result);
        }


        [Fact]

        public void Task_Customer_Feedback()
        {
            //Arrange
            var controller = new CustomerController(context);
            //Act
            var result = controller.Feedback();
            //Arrange
            Assert.IsType<ViewResult>(result);
        }

    }
}
