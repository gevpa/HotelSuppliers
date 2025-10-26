using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Suppliers.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public string AFM { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = true;
        public SupplierCategory? Category { get; set; }
        public Country? Country { get; set; }
        //public int Id { get; internal set; }
    }
}