using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreHotelRoomBookingUserPanel.Helper;
using coreHotelRoomBookingUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace coreHotelRoomBookingUserPanel.Controllers
{
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public BookingController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        //coreHotelRoomBookingFinalDatabaseContext context = new coreHotelRoomBookingFinalDatabaseContext();
        [Route ("index")]
        public IActionResult Index()
        {
                var booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                ViewBag.booking = booking;

                

                if (ViewBag.booking == null)
                {
                    return View("EmptyCart");
                }
                else
                {
                    int days = int.Parse(TempData["days"].ToString());
                    if (days == 0)
                    {
                    days++;

                    }
                    ViewBag.days = days;
                    ViewBag.total = booking.Sum(item => item.HotelRooms.RoomPrice * item.Quantity * days );
                }
                return View();
           
        }
        
        [Route("buy/{id}")]
        public IActionResult Buy (int id)
        {

                if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking") == null)
                {
                    List<Item> booking = new List<Item>();
                    booking.Add(new Item
                    {
                        HotelRooms = _context.HotelRooms.Find(id),
                        Quantity = 1
                    });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Booking", booking);
                }
                else
                {
                    List<Item> booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        booking[index].Quantity++;
                    }
                    else
                    {
                        booking.Add(new Item
                        {
                            HotelRooms = _context.HotelRooms.Find(id),
                            Quantity = 1
                        });
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "Booking", booking);
                    }

                }

                HotelRooms hotelRoom = _context.HotelRooms.Where(x => x.RoomId == id).SingleOrDefault();
                ViewBag.Hotel = hotelRoom;
                int hId = ViewBag.Hotel.HotelId;
                return RedirectToAction("HotelRoomsIndex", "Home", new { @id = hId });

        }


        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
      


                List<Item> booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                int index = isExist(id);
                booking.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Booking", booking);
                int count = 0;
                foreach (var item in booking)
                {
                    count++;
                }

                if (count != 0)
                {
                    int j = int.Parse(HttpContext.Session.GetString("CartItem"));
                    j--;
                    HttpContext.Session.SetString("CartItem", j.ToString());
                }
                else
                {
                    HttpContext.Session.Remove("CartItem");
                    if (index == 0)
                    {
                        return View("EmptyCart");

                    }

                }
                return RedirectToAction("Index");
            
        }
            private int isExist(int id)
            {


            List<Item> booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
            for (int i = 0; i < booking.Count; i++)
            {
                if (booking[i].HotelRooms.RoomId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
            }

        [Route("emptycart")]
        [HttpGet]
        public IActionResult EmptyCart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkout(int id)
        {


                var customers = _context.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
                TempData["cid"] = customers.CustomerId;


                
                var booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");

                if (booking == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                int days = int.Parse(TempData["days"].ToString());
                ViewBag.days = days;
                ViewBag.booking = booking;

                    ViewBag.total = booking.Sum(item => item.HotelRooms.RoomPrice * item.Quantity * days);
                    TempData["total"] = ViewBag.total;

                    return View(customers);
                }

        }
        [HttpPost]
        public IActionResult Checkout(string stripeEmail, string stripeToken)
        {


                var amount = TempData["total"];
                var cid = (TempData["cid"]).ToString();
                int.Parse(HttpContext.Session.GetString("cID"));


                DateTime cin = DateTime.Parse(HttpContext.Session.GetString("CheckIn"));
                DateTime cout = DateTime.Parse(HttpContext.Session.GetString("CheckOut"));

                Bookings bookings = new Bookings()
                {
                    TotalAmount = Convert.ToSingle(amount),
                    BookingDate = DateTime.Now,
                    CheckIn = cin,
                    CheckOut = cout,
                    CustomerId = int.Parse(cid)
                };

                ViewBag.Book = bookings;
                _context.Bookings.Add(bookings);
                _context.SaveChanges();
                TempData["bookingId"] = bookings.BookingId;

                var booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                List<BookingRecords> bookingRecords = new List<BookingRecords>();
                for (int i = 0; i < booking.Count; i++)
                {
                    BookingRecords bookingRecord = new BookingRecords()
                    {
                        BookingId = bookings.BookingId,
                        RoomId = booking[i].HotelRooms.RoomId,
                        Quantity = booking[i].Quantity
                    };
                    bookingRecords.Add(bookingRecord);
                }

                bookingRecords.ForEach(n => _context.BookingRecords.Add(n));
                _context.SaveChanges();
                TempData["cust"] = cid;
                ViewBag.booking = null;


                int days = int.Parse(TempData["days"].ToString());
                var customers = new CustomerService();
                var charges = new ChargeService();




                var customer = customers.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    SourceToken = stripeToken
                });


                var charge = charges.Create(new ChargeCreateOptions
                {

                    Amount = booking.Sum(item => item.HotelRooms.RoomPrice * item.Quantity * days),
                    Description = "Sample Charge",
                    Currency = "INR",
                    CustomerId = customer.Id
                });
                _context.SaveChanges();

                var customerService = new CustomerService();
                ViewBag.details = charge.PaymentMethodDetails.Card.Last4;


                Payments payment = new Payments()
                {
                    PaymentDate = DateTime.Now,
                    PaymentAmount = booking.Sum(item => item.HotelRooms.RoomPrice * item.Quantity * days),
                    PaymentDescription = " Payment Is Done.Thanks",
                    CustomerId = int.Parse(HttpContext.Session.GetString("cID")),
                    BookingId = int.Parse(TempData["bookingId"].ToString()),
                    PaymentCardNumber = int.Parse(ViewBag.details),
                    StripePaymentId = stripeToken
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();
                TempData["paymentID"] = payment.PaymentId;

                return RedirectToAction("invoice", "Booking");
                //return RedirectToAction("charge","Booking");

        }

        [Route("orderhistory")]
        public IActionResult OrderHistory()
        {
           


                int custId = int.Parse(HttpContext.Session.GetString("cID"));
                var bking = _context.Bookings.Where(x => x.CustomerId == custId).ToList();
                ViewBag.OHistory = bking;
                return View();
            
           

        }

        [Route("historydetails/{id}")]
        public IActionResult HistoryDetails(int id)
        {


                var bking = _context.BookingRecords.Where(x => x.BookingId == id).ToList();
                ViewBag.HistoryDetails = bking;
                List<HotelRooms> hotelroom = new List<HotelRooms>();

                foreach (var item in ViewBag.HistoryDetails)
                {
                    int idd = Convert.ToInt32(item.RoomId);
                    HotelRooms hr = _context.HotelRooms.Where(x => x.RoomId == idd).SingleOrDefault();
                    hotelroom.Add(hr);
                }
                ViewBag.HotelRoom = hotelroom;
                return View();

        }


        [Route("invoice")]
        [HttpGet]
        public IActionResult Invoice()
        {


                int custId = int.Parse(TempData["cust"].ToString());
                Customers customers = _context.Customers.Where(x => x.CustomerId == custId).SingleOrDefault();
                ViewBag.Customers = customers;
                var booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");

                int days = int.Parse(TempData["days"].ToString());
                ViewBag.days = int.Parse(TempData["days"].ToString());
                ViewBag.total = booking.Sum(item => item.HotelRooms.RoomPrice * days);

                ViewBag.booking = booking;
                ViewBag.paymentId = int.Parse(TempData["paymentID"].ToString());


                SessionHelper.SetObjectAsJson(HttpContext.Session, "Booking", null);
                HttpContext.Session.Remove("CartItem");
                return View();

        }


        public IActionResult Error()
        {
            return View();
        }
    }

}