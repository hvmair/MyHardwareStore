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
    public class BaseTier
    {
        public string query { get; set; }
        public SqlConnection conn { get; set; }
        public SqlCommand cmd { get; set; }
        public SqlDataReader reader { get; set; }
        public string connectionString { get; set; }

        public bool success { get; set; }
             
        public BaseTier()
        {

          connectionString = ConfigurationManager.ConnectionStrings["MyData"].ToString();

        }

    }
}