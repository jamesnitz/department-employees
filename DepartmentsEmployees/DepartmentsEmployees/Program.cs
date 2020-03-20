using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;
using System;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setting a new instance of the employee repository and dept repo to a variable. Remeber to use the using statement at the top!
            var repo = new EmployeeRepository();
            var deptRepo = new DepartmentRepository();
            //That both classes has method that gets all employees. Use it and store all them employees in a var.
            var departments = deptRepo.GetAllDepartments();
            var employees = repo.GetAllEmployees();

            Department legal = new Department()
            {
                DeptName = "Legal"
            };

            //Use loops to write line
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} is in {employee.Department.DeptName}!!");
            };

            //Use the method that gets a single employee by id and pass in an Id and set it to var.
            var employeeWithId2 = repo.GetEmployeeById(2);
            //Write it out
            Console.WriteLine($"Employee with Id 2 is {employeeWithId2.FirstName} {employeeWithId2.LastName}");

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DeptName}");
            };

            var deptWithId3 = deptRepo.GetDepartmentById(3);
            Console.WriteLine($"Department numbah 3 is {deptWithId3.DeptName}");

            //Adds a new department
            //deptRepo.AddDepartment(legal);

        }
    }
}
