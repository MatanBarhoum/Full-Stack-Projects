using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace FullStackReportSystem.Pages.Reports
{
    public class EditModel : PageModel
    {
        public Reports reportInfo = new Reports();
        private IWebHostEnvironment _environment;
        public string SuccessMessage = "";
        public string ErrorMessage = "";
        string connectionString = "Data Source=localhost;Initial Catalog=reportsystem;Integrated Security=True";
        private string _ImagePath = "";

        public EditModel (IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }
        public void OnGet()
        {
            var id = Request.Query["id"].ToString();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = "SELECT * FROM reports WHERE id=@id;";
                    using (var cmd = new SqlCommand(command, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportInfo.ReportCategory = reader.GetString(1);
                                reportInfo.FullName = reader.GetString(2);
                                reportInfo.Fee = reader.GetString(3);
                                reportInfo.payment_status = reader.GetString(4);
                                reportInfo.ImagePath = reader.GetString(6);
                            }
                        }

                    }
                    connection.Close();
                }
            }
            catch (Exception ex) { }
        }

        public void OnPost(List<IFormFile> postedFiles) // In charge for processing the file uploading..
        {
            try
            {

                string wwwPath = this._environment.WebRootPath; // using the IWebHostEnvironment to get the current working folder.
                string contentPath = this._environment.ContentRootPath; 

                string path = Path.Combine(this._environment.WebRootPath, "Uploads"); // takes the wwwRoot and the uploads folder inside it.
                if (!Directory.Exists(path)) //check if the folder exist or not, if exist send 
                {
                    Directory.CreateDirectory(path); // if ! (not) the folder will be created.
                }

                List<string> uploadedFiles = new List<string>(); // creating a list to store the filesnames, can be used to display a message with the file name.
                foreach (IFormFile postedFile in postedFiles) // 
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(fileName);
                        _ImagePath = fileName;
                    }
                }

                var id = Request.Query["id"].ToString();
                reportInfo.ReportCategory = Request.Form["Category"];
                reportInfo.FullName = Request.Form["Name"];
                reportInfo.Fee = Request.Form["Fee"];
                reportInfo.payment_status = Request.Form["payment_status"];
                reportInfo.ImagePath = _ImagePath;

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = "UPDATE reports " +
                        "SET category=@category, fullname=@fullname, fee=@fee, payment_status=@payment_status, ImagePath=@ImagePath " +
                        "WHERE id=@id;";
                    connection.Open();
                    using (var cmd = new SqlCommand(command, connection))
                    {
                        cmd.Parameters.AddWithValue("@category", reportInfo.ReportCategory);
                        cmd.Parameters.AddWithValue("@fullname", reportInfo.FullName);
                        cmd.Parameters.AddWithValue("@fee", reportInfo.Fee);
                        cmd.Parameters.AddWithValue("@payment_status", reportInfo.payment_status);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@ImagePath", reportInfo.ImagePath);

                        using (var sda = new SqlDataAdapter(cmd))
                        {
                            var dataTable = new DataSet();
                            sda.Fill(dataTable);
                            SuccessMessage = "Report Updated Successfully";
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorMessage = "Report Failed To Update"; }
        }
    }
}
