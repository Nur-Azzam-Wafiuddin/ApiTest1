using System.Runtime.CompilerServices;

namespace ApiTest1.Model
{
    public class Account
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int productHistoryId { get; set; }
        public List<Product> productHistory { get; set; }
    }
}
