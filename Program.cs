using Newtonsoft.Json;
using SeatManagement.Controllers;
using SeatManagement.Models;
using SeatManagementConsole.Managers;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.CodeAnalysis.Completion;

namespace SeatManagementConsole
{
    
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Asset management system\n");
            int choice;
            do
            {
                Console.WriteLine("\nPlease enter an option: " +
                    "\n1. Onboard a facility" +
                    "\n2. Onboard meeting room" +
                    "\n3. Onboard cabin" +
                    "\n4. Onboard seats" +
                    "\n5. Upload employee details" +
                    "\n6. Allocate seats" +
                    "\n0. EXIT");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        OnBoardFacility();
                        break;
                    case 2:
                        OnboardMeetingRoom();
                        break;
                    case 3:
                        OnBoardCabin();
                        break;
                    case 4:
                        OnBoardSeats();
                        break;
                    case 5:
                        UploadEmployeeDetails();
                        break;
                    case 6:
                        AllocateSeats();
                        break;
                    case 0:
                        //Environment.Exit(0);
                        break;
                }

            } while (choice != 0);
            //func();
            Console.ReadLine();
            IEntityManager<City> city = new EntityManager<City>("cities/");
            var s = new City 
            { 
                Name= "Kollam",
                Abbreviation = "KLM"
            }; 
            city.Add(s);

            var cities = city.Get();
            foreach(var c in cities)
            {
                Console.WriteLine(c.Name);
            }
            Console.ReadLine();

        }
        public static void OnBoardFacility()
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

        public static void OnboardMeetingRoom()
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

        public static void OnBoardCabin()
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

        public static void OnBoardSeats()
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

        public static void UploadEmployeeDetails()
        {
            Console.WriteLine("Enter no of employees to add");
            int noOfEmployees = Convert.ToInt32(Console.ReadLine());
            List<Employee> employees = new List<Employee>();
            int i;
            for (i = 0; i < noOfEmployees; i++)
            {
                Console.WriteLine($"Enter Name of Employee {i+1}");
                string name = Console.ReadLine();
                Console.WriteLine($"Enter Department of Employee {i+1}");
                int departmentId = Convert.ToInt32(Console.ReadLine());
                Employee employee = new Employee
                {
                    Name = name,
                    DepartmentId = departmentId,
                };
                employees.Add(employee);
            }
            string _endPoint = "employees/bulkadd";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7224/api/");
            var json = JsonSerializer.Serialize(employees);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(_endPoint, content).Result;

        }

        public static void AllocateSeats()
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