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
            cmd.Parameters.Add("@Image", SqlDbType.Image).Value = product.productImage;

            if ( product.productImage != null )
            {
                cmd.Parameters.Add("@ImgType", SqlDbType.NVarChar, 50).Value = product.imageType;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.quantityOnHand;
            }
            else 
            {
                cmd.Parameters.Add("@ImgType", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = DBNull.Value;
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

    }
}