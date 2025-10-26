namespace Suppliers.DTO
{
    public class SupplierInsertDTO
    {
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; } 
        public string AFM { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CountryId { get; set; } 
        public bool? IsActive { get; set; }

    }
}
