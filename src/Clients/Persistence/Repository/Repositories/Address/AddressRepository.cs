using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Contexts;

namespace Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        private readonly string _databaseConnection;

        public AddressRepository(CustomerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _databaseConnection = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddAddressAsync(Address address)
        {
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPI_Address";

                    await conn.ExecuteAsync(sql, new { CustomerId = address.CustomerId, Place = address.Place }, commandType: System.Data.CommandType.StoredProcedure); // Ajustado os nomes dos parâmetros
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public async void UpdateAddress(Address address)
        {
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPU_Address";

                    await conn.ExecuteAsync(sql, new { AddressId = address.Id, Place = address.Place }, commandType: System.Data.CommandType.StoredProcedure); // Ajustado os nomes dos parâmetros
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void DeleteAddress(Address address) =>
            _context.Address.Remove(address);

        public Address GetAddressById(int addressId) =>
            _context.Address
                .Include(x => x.Customer).ThenInclude(u => u.Users)
                .FirstOrDefault(x => x.Id == addressId);

        public async Task<List<Address>> GetAddressesByCustomerId(int customerId)
        {
            IEnumerable<Address> list;
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPS_Address";

                    list = await conn.QueryAsync<Address>(sql, new { CustomerId = customerId }, commandType: System.Data.CommandType.StoredProcedure); // Ajustado o nome do parâmetro
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return list?.ToList();
            }
        }

    }
}
