using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using RafaelMillis.QueueEntities;

namespace RafaelMillis.QueuesDAL
{
    public class QueueDAL
    {
        public QueueDAL()
        {
            ConString = ConfigurationManager.ConnectionStrings["QueueDBConString"].ToString();
        }

        /// <summary>
        /// get active customer
        /// </summary>
        /// <returns></returns>
        public Customer GetActiveCustomer()
        {
            Customer cust = new Customer();
            using (con = new SqlConnection())
            {
                con.ConnectionString = ConString;
                SqlCommand cmd = new SqlCommand("GetCustomerActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cust.CustomerID = reader.GetInt32(0);
                    cust.QueueID = reader.GetInt32(1);
                    cust.FirstName = reader.GetString(2);
                    cust.LastName = reader.GetString(3);
                    cust.Time = DateTime.Parse(reader.GetTimeSpan(4).ToString());
                    cust.Status = reader.GetString(5);
                }
                return cust;
            }
        }

        /// <summary>
        /// get customers waiting list 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomerWaiting()
        {
            List<Customer> customers = new List<Customer>();
            using (con = new SqlConnection())
            {
                con.ConnectionString = ConString;
                SqlCommand cmd = new SqlCommand("GetCustomerWaiting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                Customer cust;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cust = new Customer();
                    cust.CustomerID = reader.GetInt32(0);
                    cust.QueueID = reader.GetInt32(1);
                    cust.FirstName = reader.GetString(2);
                    cust.LastName = reader.GetString(3);
                    cust.Time = DateTime.Parse(reader.GetTimeSpan(4).ToString());
                    cust.Status = reader.GetString(5);
                    customers.Add(cust);
                }
                return customers;
            }
        }

        /// <summary>
        /// update next customer
        /// </summary>
        public void UpdateNextCustomer()
        {
            using (con = new SqlConnection())
            {
                con.ConnectionString = ConString;
                SqlCommand cmd = new SqlCommand("UpdateCustomerNext", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// insert a new customer
        /// </summary>
        /// <param name="newCust"></param>
        public void InsertNewCustomer(Customer newCust)
        {
            using (con = new SqlConnection())
            {
                con.ConnectionString = ConString;
                SqlCommand cmd = new SqlCommand("InsertCustomerQueue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter fname = cmd.Parameters.Add("@FName", SqlDbType.NVarChar);
                fname.Value = newCust.FirstName;
                SqlParameter lname = cmd.Parameters.Add("@LName", SqlDbType.NVarChar);
                lname.Value = newCust.LastName;
                SqlParameter time = cmd.Parameters.Add("@Time", SqlDbType.DateTime);
                time.Value = newCust.Time;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private readonly string ConString;
        private SqlConnection con;
    }
}
