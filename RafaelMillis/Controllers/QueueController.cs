using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RafaelMillis.ViewModel;
using RafaelMillis.QueueEntities;
using RafaelMillis.BLL;

namespace RafaelMillis.Controllers
{
    /// <summary>
    /// Queue WebAPI 
    /// </summary>
    public class QueueController : Controller
    {
        // GET: Queue
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// get active customer which in service
        /// </summary>
        /// <returns></returns>
        public JsonResult ActiveCustomer()
        {
            QueueBLL bll = new QueueBLL();
            List<CustomerView> customer = new List<CustomerView>();
            customer.Add(bll.GetActiveCustomer());
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get customers waiting list
        /// </summary>
        /// <returns></returns>
        public JsonResult WaitingList()
        {
            QueueBLL bll = new QueueBLL();
            List<Customer> custs = bll.GetCustomerWaiting();
            List<CustomerView> custsview = new List<CustomerView>();
            CustomerView tmp;
            foreach (Customer c in custs)
            {
                tmp = new CustomerView { CustomerName = c.FirstName + " " + c.LastName, QueueID = c.QueueID, QueueTime = c.Time.ToShortTimeString() };
                custsview.Add(tmp);
            }
            return Json( custsview, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get next customer
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateNextCustomer()
        {
            QueueBLL bll = new QueueBLL();
            bll.UpdateNextCustomer();

            return WaitingList();
        }


        /// <summary>
        /// insert a new customer to the waiting list
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public JsonResult InsertNewCustomer(string firstName, string lastName)
        {
            Customer cust = new Customer { FirstName = firstName, LastName = lastName, Time = DateTime.Now };
            QueueBLL bll = new QueueBLL();
            bll.InsertNewCustomer(cust);

            return WaitingList();
        }

    }
}