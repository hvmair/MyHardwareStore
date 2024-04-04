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
    public class CustomerTier : BaseTier
    {
        public CustomerTier() : base() 
        {           
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customerList = null;
            Customer customer = null;

            query = "SELECT * FROM CustomerInformation;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows )
                {
                    customerList = new List<Customer>();
                    while ( reader.Read() )
                    {
                        customer = new Customer();

                        customer.custId = (int)reader["CustID"];
                        customer.firstName = (string)reader["FirstName"];
                        if (reader["MiddleName"] != DBNull.Value) 
                        {
                            customer.middleName = (string)reader["MiddleName"];
                        }
                        else
                        {
                            customer.middleName = "N/A";
                        }
                        customer.lastName = reader["LastName"].ToString();
                        customer.address = (string)reader["Address"];
                        if (reader["Address2"] != DBNull.Value)
                        {
                            customer.address2 = (string)reader["Address2"];
                        }
                        else
                        {
                            customer.address2 = "N/A";
                        }
                        customer.city = (string)reader["City"];
                        customer.state = (string)reader["State"];
                        customer.zipCode = (int)reader["ZipCode"];
                        customerList.Add(customer);

                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return customerList;
        }
        public bool insertCustomer(Customer customer)
        {

            bool success = false;
            int rows = 0;

            query = "INSERT INTO CustomerInformation " +
                "(FirstName, MiddleName, LastName, Address, Address2," +
                "City, State, Zip) " +
                "VALUES(@FName, @MName, @LName, @Address, @Address2, " +
                "@City, @State, @Zip ); ";
            
            conn = new SqlConnection(connectionString);

            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@FName", SqlDbType.NVarChar, 50).Value = customer.firstName;
            if (customer.middleName != null)
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = customer.middleName;
            }
            else
            {
                cmd.Parameters.Add("@MName", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@LName", SqlDbType.NVarChar, 50).Value = customer.lastName;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 50).Value = customer.address;
            if (customer.address2 != null)
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = customer.address2;
            }
            else
            {
                cmd.Parameters.Add("@Address2", SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = customer.city;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = customer.state;
            cmd.Parameters.Add("@Zip", SqlDbType.Int).Value = customer.zipCode;

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

            } catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            }

            return success;
        }

        public Customer getCustomerByID(int id)
        {
            Customer customer = null;
            query = "SELECT * FROM CustomerInformation WHERE CustID = @ID;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ID",SqlDbType.Int).Value=id;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    customer = new Customer();

                    customer.custId = (int)reader["CustID"];
                    customer.firstName = (string)reader["FirstName"];
                    if (reader["MiddleName"] != DBNull.Value)
                    {
                        customer.middleName = (string)reader["MiddleName"];
                    }else
                    {
                        customer.middleName = "N/A";
                    }
                    customer.lastName = (string)reader["LastName"];
                    customer.address = (string)reader["Address"];
                    if (reader["Address2"] != DBNull.Value)
                    {
                        customer.address2 = (string)reader["Address2"];
                    }else
                    {
                        customer.address2 = "N/A";
                    }
                    customer.city = (string)reader["City"];
                    customer.state = (string)reader["State"];
                    customer.zipCode = (int)reader["ZipCode"];


                }

            }catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return customer;
        }
    }
}