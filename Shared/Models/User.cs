namespace bpgweb.Shared.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Group Group { get; set; }
        public int GroupID { get; set; }
    }
}