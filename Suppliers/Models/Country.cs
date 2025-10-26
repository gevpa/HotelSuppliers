namespace Suppliers.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        // Navigation property
        public ICollection<Supplier>? Suppliers { get; set; }
        public object Name { get; internal set; }
    }
}