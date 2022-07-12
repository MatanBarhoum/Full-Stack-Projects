using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Http.Formatting;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    public class DepartmentController : ApiController
    {
        // GET: Department
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
    ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
            {
                var query = @"Select DepartmentName,DepartmentId from dbo.Department";

                using (var cmd = new SqlCommand(query, con))
                { 
                    using (var da = new SqlDataAdapter(cmd)) // da = dataTable, SQLDataAdapter help fills the data to the table
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table); // SqlDataAdapter fills the table created earlier.
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, table); //return the values of the database in json format as declared at WebAPIConfig.
        }

        public string Post(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = @"insert into dbo.Department values (@depName)";

                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd)) // da = dataTable, SQLDataAdapter help fills the data to the table
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@depName", dep.DepartmentName);
                        da.Fill(table); // SqlDataAdapter fills the table created earlier.
                    }
                }
                return "Added Successfully";
            }
            catch (Exception ex) { return "Failed to Add"; }
        }

        public string Put(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = @"update Department set DepartmentName=@depname where DepartmentId=@depid";

                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd)) // da = dataTable, SQLDataAdapter help fills the data to the table
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@depname", dep.DepartmentName);
                        cmd.Parameters.AddWithValue("@depid", dep.DepartmentId);
                        da.Fill(table); // SqlDataAdapter fills the table created earlier.
                    }
                }
                return "Updated Successfully";
            }
            catch (Exception ex) { return "Failed to Update"; }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = @"DELETE FROM Department where DepartmentId=@id";

                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd)) // da = dataTable, SQLDataAdapter help fills the data to the table
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);
                        da.Fill(table); // SqlDataAdapter fills the table created earlier.
                    }
                }
                return "Delete Successfully";
            }
            catch (Exception ex) { return "Failed to Delete"; }
        }
    }
}