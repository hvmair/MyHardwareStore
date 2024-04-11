using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace MyHardwareStore
{
    public class EmployeeTier : BaseTier
    {
        public EmployeeTier() : base()
        {
        }

        public List<Employee> getAllEmployee()
            {
                List<Employee> employeeList = null;
                Employee employee = null;

                query = "SELECT * FROM EmployeeInformation;";
                conn = new SqlConnection(connectionString);
                cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        employeeList = new List<Employee>();
                        while (reader.Read())
                        {
                            employee = new Employee();

                            employee.employeeId = (int)reader["EmployeeId"];
                            employee.firstName = (string)reader["FirstName"];
                            if (reader["MiddleName"] != DBNull.Value)
                            {
                                employee.middleName = (string)reader["MiddleName"];
                            }
                            else
                            {
                                employee.middleName = "N/A";
                            }
                            employee.lastName = reader["LastName"].ToString();
                            employee.address = (string)reader["Address"];
                            if (reader["Address2"] != DBNull.Value)
                            {
                                employee.address2 = (string)reader["Address2"];
                            }
                            else
                            {
                                employee.address2 = "N/A";
                            }
                            employee.city = (string)reader["City"];
                            employee.state = (string)reader["State"];
                            employee.zipCode = (int)reader["Zip"];
                            if (reader["taxID"] != DBNull.Value)
                            {
                                employee.taxID = (long)reader["TaxID"];
                            }
                            else
                            {
                                employee.taxID = 0;
                            }
                            employee.dateHired = (DateTime)reader["DateHired"];
                            employee.dateTerminated = (DateTime)reader["DateTerminated"];
                            employee.hourlyWage = (SqlMoney)reader["HourlyWage"];
                            employee.salary = (SqlMoney)reader["Salary"];
                            employee.departmentID = (int)reader["DepartmentID"];
                            employee.managerID = (int)reader["ManagerID"];
                            employeeList.Add(employee);

                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


                return employeeList;
            }
        public bool insertEmployee(Employee employee)
        {

            bool success = false;
            int rows = 0;

            query = "INSERT INTO EmployeeInformation " +
                "(FirstName, MiddleName, LastName, Address, Address2," +
                "City, State, Zip, TaxID, DateHired) " +
                "VALUES(@FName, @MName, @LName, @Address, @Address2, " +
                "@City, @State, @Zip, @TaxID, @DHired); ";

            conn = new SqlConnection(connectionString);

            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@FName", SqlDbType.NVarChar, 50).Value = employee.firstName;
            if (employee.middleName != null)
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = employee.middleName;
            }
            else
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@LName", SqlDbType.NVarChar, 50).Value = employee.lastName;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 50).Value = employee.address;
            if (employee.address2 != null)
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = employee.address2;
            }
            else
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = employee.city;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = employee.state;
            cmd.Parameters.Add("@Zip", SqlDbType.Int).Value = employee.zipCode;
            cmd.Parameters.Add("@TaxID", SqlDbType.BigInt).Value = employee.taxID;
            cmd.Parameters.Add("@DHired", SqlDbType.DateTime).Value = employee.dateHired;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
         
        }

        public Employee getEmployeeByID(int id)
        {
            Employee employee = null;
            query = "SELECT * FROM EmployeeInformation WHERE EmployeeID = @ID;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Employee();

                    employee.employeeId = (int)reader["EmployeeID"];
                    employee.firstName = (string)reader["FirstName"];
                    if (reader["MiddleName"] != DBNull.Value)
                    {
                        employee.middleName = (string)reader["MiddleName"];
                    }
                    else
                    {
                        employee.middleName = "N/A";
                    }
                    employee.lastName = (string)reader["LastName"];
                    employee.address = (string)reader["Address"];
                    if (reader["Address2"] != DBNull.Value)
                    {
                        employee.address2 = (string)reader["Address2"];
                    }
                    else
                    {
                        employee.address2 = "N/A";
                    }
                    employee.city = (string)reader["City"];
                    employee.state = (string)reader["State"];
                    employee.zipCode = (int)reader["Zip"];
                    employee.taxID = (long)reader["TaxID"];
                    employee.dateHired = (DateTime)reader["DateHired"];
                    employee.dateTerminated = (DateTime)reader["DateTerminated"];
                    employee.hourlyWage = (SqlMoney)reader["HourlyWage"];
                    employee.salary = (SqlMoney)reader["Salary"];
                    employee.departmentID = (int)reader["DepartmentID"];
                    employee.managerID = (int)reader["ManagerID"];

                }

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

    }
}