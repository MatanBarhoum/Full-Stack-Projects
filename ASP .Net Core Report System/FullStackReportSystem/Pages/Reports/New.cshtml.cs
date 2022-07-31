using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FullStackReportSystem.Pages.Reports
{
    public class NewModel : PageModel
    {
        private IWebHostEnvironment Environment;
        public List<Reports> reportsList = new List<Reports>();
        public string SuccessMessage = "";
        public string ErrorMessage = "";
        private string _ImagePath = "";

        public NewModel(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public void OnGet()
        {
        }

        public void OnPost(List<IFormFile> postedFiles)
        {
            try
            {

                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(fileName);
                        _ImagePath = fileName;
                    }
                }

                var connectionString = "Data Source=localhost;Initial Catalog=reportsystem;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = "INSERT INTO reports (category, fullname, fee, payment_status, ImagePath) " +
                        "VALUES (@category, @fullname, @fee, @payment_status, @ImagePath);";
                    using (var cmd = new SqlCommand(command, connection))
                    {
                        var reportInfo = new Reports();
                        reportInfo.ReportCategory = Request.Form["Category"];
                        reportInfo.FullName = Request.Form["Name"];
                        reportInfo.Fee = Request.Form["Fee"];
                        reportInfo.payment_status = Request.Form["payment_status"];
                        reportInfo.ImagePath = _ImagePath;

                        cmd.Parameters.AddWithValue("@category", reportInfo.ReportCategory);
                        cmd.Parameters.AddWithValue("@fullname", reportInfo.FullName);
                        cmd.Parameters.AddWithValue("@fee", reportInfo.Fee);
                        cmd.Parameters.AddWithValue("@payment_status", reportInfo.payment_status);
                        cmd.Parameters.AddWithValue("@ImagePath", reportInfo.ImagePath);

                        using (SqlDataAdapter sd = new SqlDataAdapter(cmd))
                        {
                            var _dataSet = new DataSet();
                            sd.Fill(_dataSet);
                            SuccessMessage = "Report created Succesfully";
                        }

                    }
                }
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }
    }
}
