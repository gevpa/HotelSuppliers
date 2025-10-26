using AutoMapper;
using Suppliers.DTO;
using Suppliers.Models;

namespace Suppliers.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SupplierInsertDTO, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDTO, Supplier>().ReverseMap();
            CreateMap<SupplierReadOnlyDTO, Supplier>().ReverseMap();
        }
    }
}
