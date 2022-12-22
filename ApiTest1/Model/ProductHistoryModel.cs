namespace ApiTest1.Model
{
    public class ProductHistory
    {
        public int id { get; set; }
        public string name { get; set; }
        public string approval { get; set; } // Accepted or Pending or Rejected
        public string borrowPeriodStart { get; set; }
        public string borrowPeriodEnd { get; set; }
    }
}
