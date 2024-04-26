using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyHardwareStore
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }

        public decimal productPrice { get; set; }

        public byte[] productImage { get; set; }

        public string imageType { get; set; } 

        public int quantityOnHand { get; set; }
        
        public int categoryID { get; set; }

        public int departmentID { get; set; }


    }
}