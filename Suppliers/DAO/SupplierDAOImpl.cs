using Suppliers.Models;
using Suppliers.Services.DBHelper;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace Suppliers.DAO
{
    public class SupplierDAOImpl : ISupplierDAO
    {
        public IList<Supplier> GetAll()
        {
            string sql = "SELECT * FROM dbo.Suppliers";
            var suppliers = new List<Supplier>();

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Supplier supplier = new()
                {
                    SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                    AFM = reader.GetString(reader.GetOrdinal("AFM")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
                suppliers.Add(supplier);
            }
            return suppliers;
        }

        public Supplier? GetById(int id)
        {
            string sql = "SELECT * FROM dbo.Suppliers WHERE SupplierId = @SupplierId";
            Supplier? supplier = null;

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@SupplierId", id);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                supplier = new()
                {
                    SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                    AFM = reader.GetString(reader.GetOrdinal("AFM")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
            }
            return supplier;
        }

        public Supplier? Insert(Supplier suppliers)
        {
            if (suppliers is null) return null;
            string sql = "INSERT INTO dbo.Suppliers (Name, CategoryId, AFM, Address, Phone, Email, CountryId, IsActive) VALUES (@Name, @CategoryId, @AFM, @Address, @Phone, @Email, @CountryId, @IsActive); SELECT SCOPE_IDENTITY();";

            Supplier? supplierToReturn = null;

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand insertCommand = new(sql, conn);
            insertCommand.Parameters.AddWithValue("@Name", suppliers.Name);
            insertCommand.Parameters.AddWithValue("@CategoryId", suppliers.CategoryId);
            insertCommand.Parameters.AddWithValue("@AFM", suppliers.AFM);
            insertCommand.Parameters.AddWithValue("@Address", suppliers.Address);
            insertCommand.Parameters.AddWithValue("@Phone", suppliers.Phone);
            insertCommand.Parameters.AddWithValue("@Email", suppliers.Email);
            insertCommand.Parameters.AddWithValue("@CountryId", suppliers.CountryId);
            insertCommand.Parameters.AddWithValue("@IsActive", suppliers.IsActive);

            object insertedObj = insertCommand.ExecuteScalar();
            int insertedId = 0;
            if (insertedObj is not null && int.TryParse(insertedObj.ToString(), out insertedId))
            {
                string sqlInsertedSupplier = "SELECT * FROM dbo.Suppliers WHERE SupplierId = @SupplierId";

                using SqlCommand selectCommand = new(sqlInsertedSupplier, conn);
                selectCommand.Parameters.AddWithValue("@SupplierId", insertedId);

                using SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    supplierToReturn = new()
                    {
                        SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        AFM = reader.GetString(reader.GetOrdinal("AFM")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                }
            }
            return supplierToReturn;
        }

        public Supplier? Update(Supplier? suppliers)
        {
            if (suppliers is null) return null;
            string sql = "UPDATE dbo.Suppliers SET Name = @Name, CategoryId = @CategoryId, AFM = @AFM, Address = @Address, Phone = @Phone, Email = @Email, CountryId = @CountryId, IsActive = @IsActive WHERE SupplierId = @SupplierId";

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand updateCommand = new(sql, conn);
            updateCommand.Parameters.AddWithValue("@Name", suppliers.Name);
            updateCommand.Parameters.AddWithValue("@CategoryId", suppliers.CategoryId);
            updateCommand.Parameters.AddWithValue("@AFM", suppliers.AFM);
            updateCommand.Parameters.AddWithValue("@Address", suppliers.Address);
            updateCommand.Parameters.AddWithValue("@Phone", suppliers.Phone);
            updateCommand.Parameters.AddWithValue("@Email", suppliers.Email);
            updateCommand.Parameters.AddWithValue("@CountryId", suppliers.CountryId);
            updateCommand.Parameters.AddWithValue("@IsActive", suppliers.IsActive);
            updateCommand.Parameters.AddWithValue("@SupplierId", suppliers.SupplierId);
            updateCommand.ExecuteNonQuery();
            return suppliers;
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM dbo.Suppliers WHERE SupplierId = @SupplierId";

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand deleteCommand = new(sql, conn);
            deleteCommand.Parameters.AddWithValue("@SupplierId", id);
            deleteCommand.ExecuteNonQuery();
        }

        public Supplier? GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
