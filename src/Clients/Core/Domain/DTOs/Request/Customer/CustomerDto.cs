namespace Domain.DTOs
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }

        public List<AddressDto> Places { get; set; }
    }
}
