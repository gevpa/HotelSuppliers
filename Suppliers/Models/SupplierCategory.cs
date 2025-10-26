namespace Suppliers.Models
{
    public class SupplierCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation property
        public ICollection<Supplier>? Suppliers { get; set; }
        public object Name { get; internal set; }
    }
}