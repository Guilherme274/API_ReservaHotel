using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Data
{
    public class APIContext : DbContext
    {
        
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }
        public DbSet<HotelBooking> Bookings { get; set; }
    }
}
