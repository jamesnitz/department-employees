using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;
using System;
using System.Linq;

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
           

            var employeeWithId2 = repo.GetEmployeeById(2);
          

            while (true)
            {
                var departments = deptRepo.GetAllDepartments();
                var employees = repo.GetAllEmployees();
                Console.WriteLine("Welcome to muh Database!!");
                Console.WriteLine("Press 1 for Departments");
                Console.WriteLine("Press 2 for Employee");
                Console.WriteLine("Press 3 for a full report");
                Console.WriteLine("Press 4 to get outta here");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Clear();
                    Console.WriteLine("---DEPARTMENTS---");
                    Console.WriteLine("Press 1 to add a Department");
                    Console.WriteLine("Press 2 to add a Delete");
                    Console.WriteLine("Press 3 to return");
                    string deptOption = Console.ReadLine();

                    switch (Int32.Parse(deptOption))
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Name of Department?");
                            var deptNameInput = Console.ReadLine();
                            Department newDepartment = new Department() { DeptName = deptNameInput };
                            deptRepo.AddDepartment(newDepartment);
                          break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("Delete which Department?");
                            for (var i = 0; i < departments.Count; i++)
                            {
                                Console.WriteLine($"{departments[i].Id}  {departments[i].DeptName}");
                            }
                            var deleteDeptInput = Int32.Parse(Console.ReadLine());
                            try
                            {
                                deptRepo.DeleteDepartment(deleteDeptInput);
                                break;
                            }
                            catch
                            {
                                
                                Console.Clear();
                                Console.WriteLine("Cannot delete department with working employees.");
                                Console.WriteLine("Please fire or transfer:");
                                Console.WriteLine("");
                                foreach(var employee in employees)
                                {
                                    if (employee.DepartmentId == deleteDeptInput)
                                    {
                                        Console.WriteLine($"{employee.FirstName} {employee.LastName}");
                                    }
                                }
                                Console.WriteLine("Press enter to return");
                                Console.ReadLine();
                                break;
                            }
                        case 3:
                            break;

                        default:
                            break;
                    }
                }
                else if (option == "2")
                {
                    Console.Clear();
                    Console.WriteLine("---EMPLOYEES---");
                    Console.WriteLine("Press 1 to add an employee");
                    Console.WriteLine("Press 2 to Fire an Employee");
                    Console.WriteLine("Press 3 to update an employee");
                    Console.WriteLine("Press 4 to return");
                    string empOption = Console.ReadLine();

                    switch (Int32.Parse(empOption))
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("First name of Employee?");
                            var employeeFirstNameInput = Console.ReadLine();
                            Console.WriteLine("Last Name of Employee?");
                            var employeeLastNameInput = Console.ReadLine();
                            Console.WriteLine("Which Department do they work in?");
                            foreach(var dept in departments)
                            {
                                Console.WriteLine($"{dept.Id} {dept.DeptName}");
                            }
                            var employeeDeptChoice = Console.ReadLine();
                            var selectedDept = departments.Where(dept => int.Parse(employeeDeptChoice) == dept.Id).FirstOrDefault();

                            Employee newEmployee = new Employee() { FirstName = employeeFirstNameInput, LastName = employeeLastNameInput, DepartmentId = selectedDept.Id };
                            repo.AddEmployee(newEmployee);
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("Fire which Employee?");
                            for (var i = 0; i < employees.Count; i++)
                            {
                                Console.WriteLine($"{employees[i].Id}  {employees[i].FirstName} {employees[i].LastName}");
                            }
                            var deleteEmployeeInput = Int32.Parse(Console.ReadLine());
                            repo.DeleteEmployee(deleteEmployeeInput);
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Who you want to update?");
                            for (var i = 0; i < employees.Count; i++)
                            {
                                Console.WriteLine($"{employees[i].Id}  {employees[i].FirstName} {employees[i].LastName}");
                            }
                            var updateEmployeeInput = Int32.Parse(Console.ReadLine());
                            var selectedEmployee = repo.GetEmployeeById(updateEmployeeInput);
                            Console.Clear();
                            Console.WriteLine($"{selectedEmployee.FirstName} {selectedEmployee.LastName} who works in {selectedEmployee.Department.DeptName}");
                            Console.WriteLine("");
                            Console.WriteLine("What would you like to update?");
                            Console.WriteLine("1. Name");
                            Console.WriteLine("2. Department");
                            var selectedUpdateInput = Console.ReadLine();
                            switch(Int32.Parse(selectedUpdateInput))
                            {
                                case 1:
                                    Console.WriteLine("Enter in new first name?");
                                    var newFirstName = Console.ReadLine();
                                    Console.WriteLine("Enter in new last name?");
                                    var newLastName = Console.ReadLine();
                                    selectedEmployee.FirstName = newFirstName;
                                    selectedEmployee.LastName = newLastName;
                                    repo.UpdateEmployee(selectedEmployee.Id, selectedEmployee);
                                    break;
                                case 2:
                                    Console.WriteLine("What department do they work in now?");
                                    for (var i = 0; i < departments.Count; i++)
                                    {
                                        Console.WriteLine($"{departments[i].Id}  {departments[i].DeptName}");
                                    }
                                    var selectedDeptInput = Int32.Parse(Console.ReadLine());

                                    var newDept = departments.First(dept => dept.Id == selectedDeptInput);
                                    selectedEmployee.DepartmentId = newDept.Id;
                                    repo.UpdateEmployee(selectedEmployee.Id, selectedEmployee);
                                    break;

                                default:
                                    break;
                            }




                            break;
                        case 4:
                            break;

                        default:
                            break;
                    }
                }
                else if (option == "3")
                {
                    Console.Clear();
                    Console.WriteLine("------------------");
                    foreach(var dept in departments)
                    {
                      Console.WriteLine($"{dept.DeptName} has the following employees:");
                      foreach(var employee in employees)
                        {
                            if (employee.DepartmentId == dept.Id)
                            {
                                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
                            }
                        }
                    }
                    Console.WriteLine("------------------");
                }
                else
                {
                    Console.WriteLine("See ya Later");
                    break;
                }

            }





        }
    }
}
