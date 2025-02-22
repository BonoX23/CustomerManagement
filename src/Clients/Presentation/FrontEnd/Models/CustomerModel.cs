namespace FrontEnd.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }

        public List<UserModel> Users { get; set; } = new();
        public List<AddressModel> Places { get; set; } = new();
    }
}
