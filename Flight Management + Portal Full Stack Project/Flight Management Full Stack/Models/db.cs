using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Flight_Management_Full_Stack.Models;

namespace Flight_Management_Full_Stack.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=FlightBeFS;Integrated Security=True");
        public bool LoginCheck(Login ad)
        {
            SqlCommand com = new SqlCommand("SELECT * FROM login WHERE username=@username and password=@password ", con);
            com.Parameters.AddWithValue("@username", ad.username);
            com.Parameters.AddWithValue("@password", ad.password);
            con.Open();
            var reader = com.ExecuteReader(); 
            if (!reader.Read())
            {
                return false;
            }
            reader.DisposeAsync();
            reader.Close();
            con.Close();
            return true;
        }

    }
}
