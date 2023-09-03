using SeatManagement.Models;
using System.Text;
using System.Text.Json;

namespace SeatManagementConsole
{
    public class UploadEmployeeDetails : IDoWork
    {
        public int WorkType => 5;

        public void Do()
        {
            Console.WriteLine("Enter no of employees to add");
            int noOfEmployees = Convert.ToInt32(Console.ReadLine());
            List<Employee> employees = new List<Employee>();
            int i;
            for (i = 0; i < noOfEmployees; i++)
            {
                Console.WriteLine($"Enter Name of Employee {i + 1}");
                string name = Console.ReadLine();
                Console.WriteLine($"Enter Department of Employee {i + 1}");
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
    }
}
