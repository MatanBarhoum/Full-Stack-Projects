using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Crud_ASP.Net_Core.Pages.Client
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
           String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=CrudASPCoreTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String cmd = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) { errorMessage = ex.Message; }

        }

        public void OnPost()
        {
            String id = Request.Query["id"];
            try {
                clientInfo.name = Request.Form["name"];
                clientInfo.email = Request.Form["email"];
                clientInfo.phone = Request.Form["phone"];
                clientInfo.address = Request.Form["address"];

                if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                    clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
                {
                    errorMessage = "All field are required!";
                    return; // will not execute further code, means of something wrong
                            //sql will not execute.
                }

                String connectionString = "Data Source=localhost;Initial Catalog=CrudASPCoreTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string cmd = "UPDATE clients " +
                        "SET name=@name, email=@email, phone=@phone, address=@address " +
                        "WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                        {
                            var dataSet = new DataSet();
                            sda.Fill(dataSet);
                        }
                    }
                    connection.Close();
                    successMessage = "Updated Succesfully";
                    Response.Redirect("/Client");
                }
            }
            catch (Exception ex) { errorMessage = ex.Message; }
        }   
    }
}
