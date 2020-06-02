namespace Alborz.DomainLayer.DTO
{
    public partial class AddressDTO : BaseDTO<int>
    {
        public string AddressType { get; set; }
        public int? Province { get; set; }
        public int? City { get; set; }
        public string PostalCode { get; set; }
        public string FirstNameReciver { get; set; }
        public string LastNameReciver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? CustomerId { get; set; }
    }
}
