using SeatManagement.Models;
using SeatManagementConsole.Managers;

namespace SeatManagementConsole
{
    public class OnBoardFacility : IDoWork
    {
        public int WorkType => 1;
        public void Do()
        {
            Console.WriteLine("Enter City of Facility: ");
            IEntityManager<City> city = new EntityManager<City>("cities/");
            var cities = city.Get();
            foreach (var c in cities)
            {
                Console.WriteLine($"{c.Id} {c.Name}");
            }
            int cityId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter name of building: ");
            IEntityManager<Building> building = new EntityManager<Building>("buildings/");
            var buildings = building.Get();
            foreach (var b in buildings)
            {
                Console.WriteLine($"{b.Id} {b.Name}");
            }
            int buildingId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Floor Number");
            int floorNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter name of facility");
            string facilityName = Console.ReadLine();

            IEntityManager<Facility> facility = new EntityManager<Facility>("facilities/");
            var f = new Facility
            {

                Name = facilityName,
                FloorNumber = floorNumber,
                CityId = cityId,
                BuildingId = buildingId
            };
            facility.Add(f);
        }
    }
}
