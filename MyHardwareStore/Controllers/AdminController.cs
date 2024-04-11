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
    }
}