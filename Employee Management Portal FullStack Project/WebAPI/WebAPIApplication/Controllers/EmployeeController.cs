using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Data;
using WebAPIApplication.Models;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Net;

namespace WebAPIApplication.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
            {
                var query = "SELECT EmployeeId,EmployeeName,Department,DateOfJoining,PhotoFileName from dbo.Employee";
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee employee)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = "INSERT INTO Employee VALUES(@EmployeeName,@Department,@DateOfJoining,'Attachment.jpg')";
                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                        cmd.Parameters.AddWithValue("@Department", employee.Department);
                        cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                        da.Fill(table);
                    }
                }
                return "Added Successfully";
            }
            catch (Exception ex) { return "Not added."; }
        }

        public string Put(Employee employee)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = "UPDATE Employee SET EmployeeName=@EmployeeName,Department=@Department,DateOfJoining=@DateOfJoining,PhotoFileName='Attachment.jpg' WHERE EmployeeId=@id";
                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", employee.EmployeeId);
                        cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                        cmd.Parameters.AddWithValue("@Department", employee.Department);
                        cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                        da.Fill(table);
                    }
                }
                return "Updated Successfully";
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
                {
                    var query = "DELETE FROM Employee WHERE EmployeeId=@id";
                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);

                        da.Fill(table);
                    }
                }
                return "Deleted Successfully";
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        [System.Web.Http.Route("api/Employee/GetAllDepartmentNames")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeWebAppDB"].ConnectionString))
            {
                var query = "Select DepartmentName from dbo.Department";
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [System.Web.Http.Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var PhysicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(PhysicalPath);

                return fileName;
            }
            catch (Exception e) { return e.ToString(); }
        }

    }
}