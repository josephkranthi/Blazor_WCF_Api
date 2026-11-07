using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace WcfService1
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Database db;

        public EmployeeService()
        {
            db = DatabaseFactory.CreateDatabase("EmployeeDB");
        }

        // GET ALL
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            DbCommand command = db.GetStoredProcCommand("GetEmployees");

            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }

            return employees;
        }

        // GET BY ID
        public Employee GetEmployeeById(int id)
        {
            DbCommand command = db.GetStoredProcCommand("GetEmployeeById");

            db.AddInParameter(command,"@Id", DbType.Int32, id);

            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                }
            }

            return null;
        }

        // INSERT
        public int AddEmployee(Employee employee)
        {
            DbCommand command = db.GetStoredProcCommand("InsertEmployee");

            db.AddInParameter( command, "@Name", DbType.String, employee.Name);

            db.AddInParameter(command, "@Salary", DbType.Decimal, employee.Salary);

            object result = db.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }

        // UPDATE
        public bool UpdateEmployee(Employee employee)
        {
            DbCommand command = db.GetStoredProcCommand("UpdateEmployee");

            db.AddInParameter( command, "@Id", DbType.Int32, employee.Id);

            db.AddInParameter( command, "@Name", DbType.String, employee.Name);

            db.AddInParameter( command, "@Salary", DbType.Decimal, employee.Salary);

            int rowsAffected = db.ExecuteNonQuery(command);

            return rowsAffected > 0;
        }

        // DELETE
        public bool DeleteEmployee(int id)
        {
            DbCommand command = db.GetStoredProcCommand("DeleteEmployee");

            db.AddInParameter( command, "@Id", DbType.Int32, id);

            int rowsAffected = db.ExecuteNonQuery(command);

            return rowsAffected > 0;
        }
    }
}
