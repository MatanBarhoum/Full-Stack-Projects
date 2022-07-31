using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FullStackReportSystem.Pages.Reports
{
    public class IndexModel : PageModel
    {
        public IWebHostEnvironment environment;
        public List<Reports> reportsList = new List<Reports>();
        public string SuccessMessage = "";
        public string ErrorMessage = "";

        public IndexModel(IWebHostEnvironment _environment)
        {
            environment = _environment;
        }
        public void OnGet()
        {
            try
            {
                var connectionString = "Data Source=localhost;Initial Catalog=reportsystem;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = "SELECT * FROM reports";
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(command, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reportInfo = new Reports();
                                reportInfo.ReportID = "" + reader.GetInt32(0);
                                reportInfo.ReportCategory = reader.GetString(1);
                                reportInfo.FullName = reader.GetString(2);
                                reportInfo.Fee = reader.GetString(3);
                                reportInfo.payment_status = reader.GetString(4);
                                reportInfo.created_at = reader.GetDateTime(5).ToString();
                                reportInfo.ImagePath = reader.GetString(6);

                                reportsList.Add(reportInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        } 
    }

    public class Reports
    {
        public string ReportID { get; set; }
        public string ReportCategory { get; set; }
        public string FullName { get; set; }
        public string Fee { get; set; }
        public string created_at { get; set; }
        public string payment_status { get; set; }
        public string ImagePath { get; set; }
    }
}
