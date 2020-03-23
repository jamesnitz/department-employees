using DepartmentsEmployees.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DepartmentsEmployees.Data
{
    public class DepartmentRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source =localhost\\SQLEXPRESS; Initial Catalog=DepartmentsEmployees; Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Department> GetAllDepartments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = @"
                        Select Id, DeptName FROM Department";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Department> allDepartments = new List<Department>();

                    while (reader.Read())
                    {
                        int idColumn = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumn);

                        int deptNameColumn = reader.GetOrdinal("DeptName");
                        string departmentNameValue = reader.GetString(deptNameColumn);

                        var department = new Department()
                        {
                            Id = idValue,
                            DeptName = departmentNameValue
                        };

                        allDepartments.Add(department);
                    }

                    reader.Close();

                    return allDepartments;
                }
            }
        }
        public void AddDepartment(Department department)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO Department (DeptName) OUTPUT INSERTED.Id Values (@deptName)";
                    cmd.Parameters.Add(new SqlParameter("@deptName", department.DeptName));
                    int id = (int)cmd.ExecuteScalar();

                    department.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }

            //DELETE department

        public void DeleteDepartment(int departmentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Department WHERE Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", departmentId));

                    cmd.ExecuteNonQuery();
                }
            }
        
        }

          public void UpdateDepartment(int departmentId, Department department)
         {
            using(SqlConnection conn = Connection)
                {
                conn.Open();
                
                using(SqlCommand cmd = conn.CreateCommand())
                    {
                    cmd.CommandText = @"
                        UPDATE department
                        SET DeptName= @DeptName;
                        Where Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@DeptName", department.DeptName));      

                    cmd.Parameters.Add(new SqlParameter("@id", departmentId));
                    // We don't expect anything back from the database(It's not a real query so we say execute non query)
                    cmd.ExecuteNonQuery();
                }
            }
         }



        public Department GetDepartmentById(int departmentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select Id, DeptName FROM Department
                         Where Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", departmentId));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int idColumn = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumn);

                        int deptNameColumn = reader.GetOrdinal("DeptName");
                        string departmentNameValue = reader.GetString(deptNameColumn);

                        var department = new Department()
                        {
                            Id = idValue,
                            DeptName = departmentNameValue
                        };

                        reader.Close();

                        return department;
                        
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


    }
}
