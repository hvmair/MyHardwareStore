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

        [Required(ErrorMessage = "Please Enter Product Name")]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Please Enter Product Price")]
        [Display(Name = "Product Price")]
        public decimal productPrice { get; set; }

        [Display(Name = "Product Image")]
        public byte[] productImage { get; set; }

        [Display(Name = "Image Type")]
        public string imageType { get; set; }

        [Display(Name = "Quantity on Hand")]
        public int quantityOnHand { get; set; }

        [Display(Name = "CategoryID")]
        public int categoryID { get; set; }

        [Display(Name = "DepartmentID")]
        public int departmentID { get; set; }


    }
}