using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHardwareStore
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllCustomers()
        {
            List<Customer> customerList = null;
            CustomerTier tier = new CustomerTier();

            customerList = tier.getAllCustomers();

            return View(customerList); 
        }

        [HttpGet]
        public ActionResult customerDetails(int id)
        {
            CustomerTier tier = new CustomerTier();
            Customer customer = tier.getCustomerByID(id);
            return View(customer); 
        }

        public ActionResult getAllEmployees()
        {
            List<Employee> employeeList = null;
            EmployeeTier tier = new EmployeeTier();

            employeeList = tier.getAllEmployee();

            return View(employeeList);
        }

        [HttpGet]
        public ActionResult employeeDetails(int id)
        {
            EmployeeTier tier = new EmployeeTier();
            Employee employee = tier.getEmployeeByID(id);
            return View(employee);
        }

        [HttpGet]
        public ActionResult editCustomer(int id)
        {
            CustomerTier tier = new CustomerTier();
            Customer customer = tier.getCustomerByID(id);
            
            return View(customer);
        }

        [HttpPost]
        public ActionResult editCustomer(Customer customer)
        {
            CustomerTier tier = new CustomerTier();
            tier.updateCustomer(customer);
            return RedirectToAction("getAllCustomers");
        }

        [HttpGet]
        public ActionResult deleteCustomer(int id)
        {
            CustomerTier tier = new CustomerTier();
            Customer customer = tier.getCustomerByID(id);

            return View(customer);

        }

        [HttpPost, ActionName("deleteCustomer")]
        public ActionResult deleteCustomerInformation(int id)
        {
            CustomerTier tier = new CustomerTier();
            tier.deleteCustomer(id);

            return RedirectToAction("getAllCustomers");
        }

        [HttpGet]
        public ActionResult editEmployee(int id)
        {
            EmployeeTier tier = new EmployeeTier();
            Employee employee = tier.getEmployeeByID(id);

            return View(employee);
        }

        [HttpPost]
        public ActionResult editEmployee(Employee employee)
        {
            EmployeeTier tier = new EmployeeTier();
            tier.updateEmployee(employee);
            return RedirectToAction("getAllEmployees");
        }

        [HttpGet]
        public ActionResult deleteEmployee(int id)
        {
            EmployeeTier tier = new EmployeeTier();
            Employee employee = tier.getEmployeeByID(id);

            return View(employee);

        }

        [HttpPost, ActionName("deleteEmployee")]
        public ActionResult deleteEmployeeInformation(int id)
        {
            EmployeeTier tier = new EmployeeTier();
            tier.deleteEmployee(id);

            return RedirectToAction("getAllEmployees");
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult AddProduct(Product product, HttpPostedFileBase fileImage)
        {
            if(ModelState.IsValid)
            {
                if(fileImage != null && fileImage.ContentLength > 0 )
                {
                    product.imageType = fileImage.ContentType;
                    product.productImage = new byte[fileImage.ContentLength];

                    int[] myArray = new int[5];

                    fileImage.InputStream.Read(product.productImage, 0, fileImage.ContentLength);
                }
                else
                {
                    product.productImage = null;
                    product.imageType = null;
                }

                ProductTier tier = new ProductTier();

                tier.insertProduct(product);

                return RedirectToAction("GetallProducts");

            }
            else
            {
                return View();
            }

        }

        public ActionResult GetAllProducts() 
        {
            ProductTier tier = new ProductTier();
            List<Product> products = tier.getAllProducts();

            return View(products);
        }

        public ActionResult ProductDetails(int id)
        {
            ProductTier tier = new ProductTier();
            Product product = tier.getProductByID(id);

            return View(product);
        }

        public ActionResult ProductImage(int id) 
        {
            ProductImage image;
            ProductTier tier = new ProductTier();

            image = tier.getProductImage(id);

            if(image != null) 
            {
                return File(image.productImage, image.imageType);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]

        public ActionResult EditProduct(int id)
        {
            ProductTier tier = new ProductTier();
            Product product = tier.getProductByID(id);

            return View(product);
        }

        [HttpPost]

        public ActionResult editProduct(Product product, HttpPostedFileBase fileImage)
        {
            if(ModelState.IsValid)
            {
                ProductTier tier = new ProductTier();
                if(fileImage != null && fileImage.ContentLength > 0)
                {
                    product.imageType = fileImage.ContentType;
                    product.productImage = new byte[fileImage.ContentLength];

                    fileImage.InputStream.Read(product.productImage, 0, fileImage.ContentLength);

                }
                else
                {
                    ProductImage image = tier.getProductImage(product.productID);
                    product.productImage = image.productImage;
                    product.imageType = image.imageType;
                }

                tier.updateProduct(product);

                return RedirectToAction("GetAllProducts");
            }
            else
            {
                return View();
            }

        }

    }
}