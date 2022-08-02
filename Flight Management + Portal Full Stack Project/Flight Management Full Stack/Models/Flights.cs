namespace Flight_Management_Full_Stack.Models
{
    public class Flights
    {
        public int id { get; set; }
        public string flightFrom { get; set; }
        public string flightDestination { get; set; }
        public string takeOff { get; set; }
        public string Landing { get; set; }
        public double price { get; set; }
        public int availableSeats { get; set; }
    }
}
