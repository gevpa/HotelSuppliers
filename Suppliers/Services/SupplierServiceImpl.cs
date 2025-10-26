using AutoMapper;
using Suppliers.DAO;
using Suppliers.DTO;
using Suppliers.Models;
using System.Transactions;

namespace Suppliers.Services
{
    public class SupplierServiceImpl : ISupplierService
    {
        private readonly ISupplierDAO _supplierDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<SupplierServiceImpl> _logger;

        public SupplierServiceImpl(ISupplierDAO supplierDAO, IMapper mapper, ILogger<SupplierServiceImpl> logger)
        {
            _supplierDAO = supplierDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public IList<Supplier> GetAllSuppliers()
        {
            try
            {
                IList<Supplier> suppliers = _supplierDAO.GetAll();
                return suppliers;
            }
            catch (Exception e) 
            {
                _logger.LogError("An error occured while fetching all suppliers: {ErrorMessage}", e.Message);
                throw;

            }
        }

        public Supplier? GetSupplier(int id)
        {
            try
            {
                return _supplierDAO.GetById(id);
               
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured while fetching one supplier: {ErrorMessage}", e.Message);
                throw;

            }
        }

        public Supplier? InsertSupplier(SupplierInsertDTO dto)
        {
            if (dto is null) return null;
            var supplier = _mapper.Map<Supplier>(dto);

            try
            {
                using TransactionScope scope = new();
                Supplier? insertedSupplier = _supplierDAO.Insert(supplier);
                scope.Complete();
                return insertedSupplier;
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured while inserting a supplier: {ErrorMessage}", e.Message);
                throw;

            }
        }

        public Supplier? UpdateSupplier(SupplierUpdateDTO dto)
        {
            if(dto is null) return null;
            Supplier? supplier = _mapper.Map<Supplier>(dto);
            Supplier? updatedSupplier = null;

            try
            {
                using TransactionScope scope = new();
                updatedSupplier = _supplierDAO.GetById(supplier.SupplierId);
                if (updatedSupplier is null) return null;
                updatedSupplier = _supplierDAO.Update(supplier);
                scope.Complete();

            }
            catch (Exception e) 
            {
                _logger.LogError("An error occured while updating a supplier: {ErrorMessage}", e.Message);
                throw;
            }

            return updatedSupplier;
        }

        public Supplier? DeleteSupplier(int id)
        {
            Supplier? supplier = null;

            try
            {
                using TransactionScope scope = new();
                supplier = _supplierDAO.GetById(id);
                if (supplier is null) return null;
                _supplierDAO.Delete(id);
                scope.Complete();

            }
            catch (Exception e)
            {
                _logger.LogError("An error occured while deleting a supplier: {ErrorMessage}", e.Message);
                throw;
            }

            return supplier;
        }

    }
}
