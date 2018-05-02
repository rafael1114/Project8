using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RafaelMillis.QueuesDAL;
using RafaelMillis.ViewModel;
using RafaelMillis.QueueEntities;

namespace RafaelMillis.BLL
{
    public class QueueBLL
    {
        QueueDAL dal = new QueueDAL();

        public CustomerView GetActiveCustomer()
        {
            Customer cust = dal.GetActiveCustomer();
            CustomerView custview = new CustomerView { CustomerName = cust.FirstName + " " + cust.LastName, QueueID = cust.QueueID, QueueTime = cust.Time.ToShortTimeString() };
            return  custview;
        }

        public List<Customer> GetCustomerWaiting()
        {
            return dal.GetCustomerWaiting();
        }

        public void UpdateNextCustomer()
        {
            dal.UpdateNextCustomer();
        }

        public void InsertNewCustomer(Customer newCust)
        {
            dal.InsertNewCustomer(newCust);
        }
    }
}