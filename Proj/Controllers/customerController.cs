using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Proj.Controllers
{
    [EnableCors(origins: "*", headers: " * ", methods: "*")]
    public class customerController : ApiController
    {
        
        //fetching data from the customer table using GET verb
        public HttpResponseMessage Get()
        {
            try { 
            //preparing the query
            string query = @"
                    select customer.location, sum(Orders.numberOfService) as sales, sum(Orders.orderAmount) as earning  from customer join Orders on customer.customerId = Orders.customerId group by customer.location order by customer.location desc
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        catch(Exception ex){
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message+ex.StackTrace);
            }
    }
        [Route("api/customer/getRecentEarnings")]
        [HttpGet]
        public HttpResponseMessage getRecentEarnings()
        {
            //preparing the query
            string query = @"
                    select dat, sum(numberOfService) as SalesCount, sum(orderAmount) as GrossEarnings, sum(orderAmount)*0.18 as TazWithhheld,
                    sum(orderAmount)-sum(orderAmount)*0.18 as NetEarnings
                    from Orders group by dat
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/customer/getCustomerNames")]
        [HttpGet]
        public HttpResponseMessage getCustomerNames()
        {
            //preparing the query
            string query = @"
                    select customerName from customer order by customerId desc
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/customer/getTransactionDetails")]
        [HttpGet]
        public HttpResponseMessage getTransactionDetails()
        {
            //preparing the query
            string query = @"
                    select * from Transactions
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/customer/getRealTimeSales")]
        [HttpGet]
        public HttpResponseMessage getRealTimeSales()
        {
            //preparing the query
            string query = @"
                    select sum(orderAmount) as TotalSales,
                    sum(orderAmount)/count(distinct(dat)) as avgsalesperday
                    from Orders
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/customer/getRetentionDetails")]
        [HttpGet]
        public HttpResponseMessage getRetentionDetails()
        {
            //preparing the query
            string query = @"
                    select sum(totalUpdateAmount) as Expansions, sum(totalCancelAmount) as Cancellation from Retention
                    ";
            //create a DataTable variable to store the data coming from the data base
            DataTable table = new DataTable();

            //write code to execute this query before that add connection string in Web.Config file
            // this is the code for instantiating the sql connection
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["projectEntities"].ConnectionString))
            // code for making  a sql command
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            // send the status code as OK and return the table containing the required data
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    }
}
