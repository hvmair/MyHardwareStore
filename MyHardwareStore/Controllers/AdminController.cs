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
            int number = id;
            return View(); 
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
            int number = id;
            return View();
        }
    }
}