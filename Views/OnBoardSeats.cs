using SeatManagement.Models;
using SeatManagementConsole.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole
{
    public class OnBoardSeats : IDoWork
    {
        public int WorkType => 4;
        public void Do()
        {
            Console.WriteLine("Enter Facility: ");
            IEntityManager<Facility> facility = new EntityManager<Facility>("facilities/");
            var facilities = facility.Get();
            foreach (var f in facilities)
            {
                Console.WriteLine($"{f.Id} {f.Name}");
            }
            int facilityId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("No of seats");
            int noOfSeats = Convert.ToInt32(Console.ReadLine());

            List<Seat> seats = new List<Seat>();
            int i;
            for (i = 0; i < noOfSeats; i++)
            {
                Seat seat = new Seat
                {
                    SeatNumber = i + 1,
                    FacilityId = facilityId,
                };
                seats.Add(seat);
            }
            string _endPoint = "seats";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7224/api/");
            var json = JsonSerializer.Serialize(seats);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(_endPoint, content).Result;
        }
    }
}
