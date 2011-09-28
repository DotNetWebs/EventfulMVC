namespace EventfulMVC.Models
{
    public class Venue : Branch
    {
        public int VenueId { get; set; }       
        public string Name { get; set; }
        public string EventfulId { get; set; }
    }
}