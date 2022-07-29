using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Crud_ASP.Net_Core.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        EditModel editModel = new EditModel();

        public void Rohan()
        {
            Console.WriteLine("123");
        }
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=CrudASPCoreTest;Integrated Security=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String cmd = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }

    public class ClientInfo
    {
        public String id { get; set; }
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }

}
