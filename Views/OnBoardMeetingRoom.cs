using SeatManagement.Models;
using SeatManagementConsole.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole
{
    public class OnBoardMeetingRoom : IDoWork
    {
        public int WorkType => 2;
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

            Console.WriteLine("Enter name of meeting room");
            string name = Console.ReadLine();

            Console.WriteLine("Enter seat count of meeting room");
            int seatCount = Convert.ToInt32(Console.ReadLine());

            IEntityManager<MeetingRoom> meetingRoom = new EntityManager<MeetingRoom>("meetingrooms/");
            var m = new MeetingRoom
            {
                Name = name,
                FacilityId = facilityId,
                SeatCount = seatCount
            };
            meetingRoom.Add(m);
        }
    }
}
