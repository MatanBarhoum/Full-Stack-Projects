using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Flight_Management_Full_Stack.Models;

namespace Flight_Management_Full_Stack.Controllers
{
    public class FlightsController : Controller
    {
        public List<Flights> flightsList = new List<Flights>();
        private string connectionString = "Data Source=localhost;Initial Catalog=FlightBeFS;Integrated Security=True";
        public IActionResult Index()
        {
            FetchData();
            return View(flightsList);
        }

        private void FetchData()
        {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var cmd = "SELECT * FROM Flights";
                    using (var command = new SqlCommand(cmd, connection))
                    {
                        var flight = new Flights();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            flight.id = reader.GetInt32(0);
                            flight.flightFrom = reader.GetString(1);
                            flight.flightDestination = reader.GetString(2);
                            flight.takeOff = reader.GetString(3);
                            flight.Landing = reader.GetString(4);
                            flight.price = reader.GetDouble(5);
                            flight.availableSeats = reader.GetInt32(6);
                            flightsList.Add(flight);

                        }
                    }
                }
            

        }
    }
}
