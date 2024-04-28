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
                            if (reader["TaxID"] != DBNull.Value)
                            {
                                employee.taxID = (long)reader["TaxID"];
                            }
                            else
                            {
                                employee.taxID = 0;
                            }
                            if (reader["DateHired"] != DBNull.Value)
                            {
                                employee.dateHired = (DateTime)reader["DateHired"];
                            }
                            else
                            {
                                employee.dateHired = new DateTime(2000, 01, 01);
                            }
                            if (reader["DateTerminated"] != DBNull.Value)
                            {
                                employee.dateTerminated = (DateTime)reader["DateTerminated"];
                            }
                            else
                            {
                                employee.dateTerminated = new DateTime(2000,01,01);
                            }
                            if(reader["HourlyWage"] != DBNull.Value)
                            {
                                employee.hourlyWage = (Decimal)reader["HourlyWage"];
                            }
                            else
                            {
                                employee.hourlyWage = 0;
                            }
                            if (reader["Salary"] != DBNull.Value)
                            {
                                employee.salary = (Decimal)reader["Salary"];
                            }
                            else
                            {
                                employee.salary = 0;
                            }
                            if (reader["DepartmentID"] != DBNull.Value)
                            {
                                employee.departmentID = (int)reader["DepartmentID"];
                            }
                            else
                            {
                                employee.departmentID = 0;
                            }
                            if (reader["ManagerID"] != DBNull.Value)
                            {
                                employee.managerID = (int)reader["ManagerID"];
                            }
                            else
                            {
                                employee.managerID = 0;
                            }
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
                    if (reader["TaxID"] != DBNull.Value)
                    {
                        employee.taxID = (long)reader["TaxID"];
                    }
                    else
                    {
                        employee.taxID = 0;
                    }
                    if (reader["DateHired"] != DBNull.Value)
                    {
                        employee.dateHired = (DateTime)reader["DateHired"];
                    }
                    else
                    {
                        employee.dateHired = new DateTime(9999, 01, 01);
                    }
                    if (reader["DateTerminated"] != DBNull.Value)
                    {
                        employee.dateTerminated = (DateTime)reader["DateTerminated"];
                    }
                    else
                    {
                        employee.dateTerminated = new DateTime(9999, 01, 01);
                    }
                    if (reader["HourlyWage"] != DBNull.Value)
                    {
                        employee.hourlyWage = (Decimal)reader["HourlyWage"];
                    }
                    else
                    {
                        employee.hourlyWage = 0;
                    }
                    if (reader["Salary"] != DBNull.Value)
                    {
                        employee.salary = (Decimal)reader["Salary"];
                    }
                    else
                    {
                        employee.salary = 0;
                    }
                    if (reader["DepartmentID"] != DBNull.Value)
                    {
                        employee.departmentID = (int)reader["DepartmentID"];
                    }
                    else
                    {
                        employee.departmentID = 0;
                    }
                    if (reader["ManagerID"] != DBNull.Value)
                    {
                        employee.managerID = (int)reader["ManagerID"];
                    }
                    else
                    {
                        employee.managerID = 0;
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

            return employee;
        }

        public bool updateEmployee(Employee employee)
        {
            bool success = false;
            int rows = 0;

            query = "UPDATE EmployeeInformation " +
                "SET FirstName = @FName, MiddleName = @MName, LastName = @LName," +
                "Address = @Address, Address2 = @Address2, City = @City, State = @State," +
                "Zip = @ZipCode, TaxID = @TaxID, DateHired = @DHired, DateTerminated = @DTerminated," +
                "HourlyWage = @HWage, Salary = @Salary, DepartmentID = @DID, " +
                "ManagerID = @MID WHERE EmployeeID = @ID;";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("ID", SqlDbType.Int).Value = employee.employeeId;
            cmd.Parameters.Add("@FName", SqlDbType.NVarChar, 50).Value = employee.firstName;
            if (employee.middleName != "N/A")
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = employee.middleName;
            }
            else
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@LName", SqlDbType.NVarChar, 50).Value = employee.lastName;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 50).Value = employee.address;
            if (employee.address2 != "N/A")
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = employee.address2;
            }
            else
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = employee.city;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = employee.state;
            cmd.Parameters.Add("@ZipCode", SqlDbType.Int).Value = employee.zipCode;
            cmd.Parameters.Add("@TaxID", SqlDbType.BigInt).Value = employee.taxID;
            cmd.Parameters.Add("@DHired", SqlDbType.DateTime).Value = employee.dateHired;
            cmd.Parameters.Add("@DTerminated", SqlDbType.DateTime).Value = employee.dateTerminated;
            cmd.Parameters.Add("@HWage", SqlDbType.Money).Value = employee.hourlyWage;
            cmd.Parameters.Add("@Salary", SqlDbType.Money).Value = employee.salary;
            cmd.Parameters.Add("@DID", SqlDbType.Int).Value = employee.departmentID;
            cmd.Parameters.Add("@MID", SqlDbType.Int).Value = employee.managerID;


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

        public bool deleteEmployee(int id)
        {
            bool success = false;
            int rows = 0;
            query = "DELETE FROM EmployeeInformation Where EmployeeID = @ID;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

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

    }
}