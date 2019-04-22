using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coreHotelRoomBookingUserPanel.Models;
using coreHotelRoomBookingUserPanel.Helper;
using Microsoft.AspNetCore.Http;

namespace coreHotelRoomBookingUserPanel.Controllers
{
    public class HomeController : Controller
    {


        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public HomeController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }
        //coreHotelRoomBookingFinalDatabaseContext context =new coreHotelRoomBookingFinalDatabaseContext() ;

        public IActionResult Index()
        {


                var hotel = _context.Hotels.ToList();
                List<Item> booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                int count = 0;
                if (booking != null)
                {
                    foreach (var item in booking)
                    {
                        count++;
                    }
                    if (count != 0)
                    {
                        HttpContext.Session.SetString("CartItem", count.ToString());
                    }

                }


                return View(hotel);


        }
        public IActionResult Details(int id)
        {


                Hotels hotel = _context.Hotels.Where(x => x.HotelId == id).SingleOrDefault();
                ViewBag.Hotel = hotel;

                int hotelTypeId = ViewBag.Hotel.HotelTypeId;
                HotelTypes hotelTypes = _context.HotelTypes.Where(x => x.HotelTypeId == hotelTypeId).SingleOrDefault();
                ViewBag.HotelType = hotelTypes;
                return View();

        }

        public IActionResult HotelRoomsIndex(int id)
        {

            List<HotelRooms> hroom = new List<HotelRooms>();
            List<RoomFacilities> rf = new List<RoomFacilities>();
            List<Item> booking = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "Booking");
                ViewBag.Booking = booking;
                int count = 0;
                if (booking != null)
                {
                    foreach (var item in booking)
                    {
                        count++;
                    }
                    if (count != 0)
                    {
                        HttpContext.Session.SetString("CartItem", count.ToString());
                    }
                }
                var hotelRoom = _context.HotelRooms.Where(x => x.HotelId == id).ToList();
                ViewBag.HotelRoom = hotelRoom;

            foreach (var item in ViewBag.HotelRoom)
            {
                int idd = item.RoomId;
                RoomFacilities rfacility = _context.RoomFacilities.Where(x => (x.IsAvilable == true) && (x.RoomId == idd)).SingleOrDefault();
                if (rfacility != null)
                {
                    rf.Add(rfacility);
                }
                

            }

            ViewBag.rf = rf;

            foreach (var item in ViewBag.rf)
            {
                int idd = item.RoomId;
                HotelRooms RoomAvailable = _context.HotelRooms.Where(x => (x.RoomId == idd)).SingleOrDefault();
                hroom.Add(RoomAvailable);
            }

            ViewBag.HotelRoomIndex = hroom;
            TempData["hotel"] = id;
                return View(hotelRoom);
 
        }

        public IActionResult RoomsDetails(int id)
        {


                int hotelId = int.Parse(TempData["hotel"].ToString());
                Hotels hotel = _context.Hotels.Where(x => x.HotelId == hotelId).SingleOrDefault();
                ViewBag.Hotel = hotel;

                HotelRooms hotelRoom = _context.HotelRooms.Where(x => x.RoomId == id).SingleOrDefault();
                ViewBag.HotelRoom = hotelRoom;
                RoomFacilities roomFacilities = _context.RoomFacilities.Where(x => x.RoomId == id).SingleOrDefault();
                ViewBag.RoomFacilities = roomFacilities;
                return View();

        }

        [Route("search")]
        [HttpGet]
        public IActionResult Search(string search, DateTime CheckIn , DateTime CheckOut)
        {

                if (search == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                //DateTime cdate = DateTime.Now;
                //int value1 = DateTime.Compare(cdate, CheckIn);
                //int value2 = DateTime.Compare(cdate, CheckIn);
                //int value3 = DateTime.Compare(CheckIn, CheckOut);

                //if (value1 < 0 && value2 < 0 )
                //{

                double days = (CheckOut - CheckIn).TotalDays;
                TempData["days"] = days;

                HttpContext.Session.SetString("Search", search.ToString());
                HttpContext.Session.SetString("CheckIn", CheckIn.ToString());
                HttpContext.Session.SetString("CheckOut", CheckOut.ToString());
                ViewBag.Hotel = _context.Hotels.Where(x => x.HotelName == search || x.HotelCity == search || x.HotelState == search || search == null).ToList();
                return View(_context.Hotels.Where(x => x.HotelName == search || search == null).ToList());
                //}
                // else
                //{
                //    ViewBag.DateMeassage = "Please Select Valid CheckIn/CheckOut Date.";
                //    return RedirectToAction("Index","Home");
                //}

        }
    }
}
