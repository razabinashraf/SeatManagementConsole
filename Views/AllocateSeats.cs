using SeatManagement.Models;
using SeatManagementConsole.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole
{
    public class AllocateSeats : IDoWork
    {
        public int WorkType => 6;

        public void Do()
        {
            Console.WriteLine("free seats are :");
            IEntityManager<Seat> seat = new EntityManager<Seat>("seats/free");
            var seats = seat.Get();
            foreach (var s in seats)
            {
                Console.WriteLine(s.SeatNumber);
                Console.WriteLine($"{s.Facility.City.Abbreviation}-{s.Facility.Building.Abbreviation}-{s.Facility.FloorNumber}-{s.Facility.Name}-S{s.SeatNumber}");
            }
            int cityId = Convert.ToInt32(Console.ReadLine());
        }
    }
}
