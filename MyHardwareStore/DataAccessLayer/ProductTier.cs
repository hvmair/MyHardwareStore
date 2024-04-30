using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data;

namespace MyHardwareStore
{
    public class ProductTier : BaseTier
    {
        public ProductTier() : base()
        {
        }
        public bool insertProduct(Product product)
        {


            query = "INSERT INTO Products (ProductName, CategoryID, QuantityOnHand, " +
                "Price, ProductImage, ImageType)" +
                "VALUES(@Name, @CID, @Quantity, @Price, @Image, @ImgType);";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = product.productName;
            cmd.Parameters.Add("@CID", SqlDbType.Int).Value = product.categoryID;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = product.productPrice;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.quantityOnHand;

            if ( product.productImage != null )
            {
                cmd.Parameters.Add("@Image", SqlDbType.Image).Value = product.productImage;
                cmd.Parameters.Add("@ImgType", SqlDbType.NVarChar, 50).Value = product.imageType;
            }
            else 
            {
                cmd.Parameters.Add("@ImgType", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                cmd.Parameters.Add("@Image", SqlDbType.Image).Value = DBNull.Value;
            }

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

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
        public List<Product> getAllProducts()
        {
            List<Product> products = null;
            Product product = null;

            query = "SELECT * FROM Products;";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    products = new List<Product>();
                    while(reader.Read())
                    {
                        product = new Product();
                        product.productID = (int)reader["ProductID"];
                        product.productName = reader["ProductName"].ToString();
                        product.productPrice = (decimal)reader["Price"];
                        product.quantityOnHand = (int)reader["QuantityOnHand"];
                        if (reader["ProductImage"] != DBNull.Value)
                        {
                            product.productImage = (byte[])reader["ProductImage"];
                            product.imageType = reader["ImageType"].ToString();
                        }
                        else
                        {
                            product.productImage = null;
                            product.imageType = null;
                        }

                        products.Add(product);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close ();
            }


            return products; 
        }

        public Product getProductByID(int id)
        {
            Product product = null;

            query = "SELECT * FROM Products WHERE ProductID =@ID;";

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
                    product = new Product();

                    product = new Product();
                    product.productID = (int)reader["ProductID"];
                    product.productName = reader["ProductName"].ToString();
                    product.productPrice = (decimal)reader["Price"];
                    product.quantityOnHand = (int)reader["QuantityOnHand"];
                    if (reader["ProductImage"] != DBNull.Value)
                    {
                        product.productImage = (byte[])reader["ProductImage"];
                        product.imageType = reader["ImageType"].ToString();
                    }
                    else
                    {
                        product.productImage = null;
                        product.imageType = null;
                    }

                }
            }
            catch (SqlException ex) 
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close ();
            }

            return product;

        }

        public ProductImage getProductImage(int id)
        {
            ProductImage image = null;

            query = "SELECT ProductImage, ImageType FROM Products WHERE ProductID = @ID;";

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
                    image = new ProductImage();

                    if (reader["ProductImage"] != DBNull.Value)
                    {
                        image.productImage = (byte[])reader["ProductImage"];
                        image.imageType = reader["ImageType"].ToString();
                    }
                    else
                    {
                        image.productImage = null;
                        image.imageType = null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception (ex.Message);
            }
            finally
            {
                conn.Close ();
            }

            return image;
        }

        public bool updateProduct(Product product)
        {
            int rows = 0;

            query = "UPDATE Products " +
                "SET ProductName = @Name, CategoryID = @CID, DepartmentID = @DID, QuantityOnHand = @Quant, " +
                "Price = @Price, ProductImage = @Image, ImageType = @Type, " +
                "WHERE ProductID = @ID;";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = product.productID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = product.productName;
            cmd.Parameters.Add("@CID", SqlDbType.Int).Value = product.categoryID;
            cmd.Parameters.Add("@DID", SqlDbType.Int).Value = product.departmentID;
            cmd.Parameters.Add("@Quant", SqlDbType.Int).Value = product.quantityOnHand;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = product.productPrice;
          



            return success;
        }

    }
}