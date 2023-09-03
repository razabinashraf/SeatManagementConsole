using SeatManagement.Models;
using SeatManagementConsole.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole
{
    public class OnBoardCabin : IDoWork
    {
        public int WorkType => 3;
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

            Console.WriteLine("Enter name of cabin room");
            string name = Console.ReadLine();


            IEntityManager<CabinRoom> cabinRoom = new EntityManager<CabinRoom>("cabinrooms/");
            var c = new CabinRoom
            {
                Name = name,
                FacilityId = facilityId,
            };
            cabinRoom.Add(c);
        }
    }
}
