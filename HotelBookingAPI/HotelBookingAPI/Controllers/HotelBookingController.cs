using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Http.Connections;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        public APIContext _context;

        public HotelBookingController(APIContext context)
        {
            _context = context;
        }

        //Criar e Editar

        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if(booking.Id >= 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingDb = _context.Bookings.Find(booking.Id);

                if(bookingDb == null)
                    return new JsonResult(NotFound());

                bookingDb = booking;
            }
            
            _context.SaveChanges();

            return new JsonResult(Ok(booking));
 
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            var jsonString = JsonSerializer.Serialize(result);
            var bookings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

            if (bookings.ContainsKey("value"))
                return new JsonResult(bookings["value"]);
            else
                return new JsonResult(bookings);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Bookings.Remove(result);

            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet()]
        public JsonResult GetAll()
        {

            var result = _context.Bookings.ToList();

            return new JsonResult(Ok(result));

            
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] HotelBooking updatedBooking)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            result.ClientsName = updatedBooking.ClientsName;
            result.Id = updatedBooking.Id;
            result.RoomNumber = updatedBooking.RoomNumber;

            _context.Update(result);
            
            _context.SaveChanges();

            return new JsonResult(result);

        }
    }
}
