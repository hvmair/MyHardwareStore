using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHardwareStore
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerRegistration(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //The form has all fields filled out and data can be sent to 
                //Database management system
                CustomerTier tier = new CustomerTier();
                tier.insertCustomer(customer);

                return View("ThankYou", customer);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmployeeRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegistration(Employee employee)
        {
            if (ModelState.IsValid)
            {
                //The form has all fields filled out and data can be sent to 
                //Database management system
                EmployeeTier tier = new EmployeeTier();
                tier.insertEmployee(employee);

                return View("ThankYouEmp", employee);
            }
            else
            {
                return View();
            }
        }

    }
}