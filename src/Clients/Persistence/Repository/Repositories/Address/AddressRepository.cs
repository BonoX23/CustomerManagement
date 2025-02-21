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
            await using var conn = new SqlConnection(_databaseConnection);
            try
            {
                await conn.OpenAsync();

                var sql = "SPI_Address";

                await conn.ExecuteAsync(sql,
                    new { CustomerId = address.CustomerId, Place = address.Place },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAddress(Address address)
        {
            await using var conn = new SqlConnection(_databaseConnection);
            try
            {
                await conn.OpenAsync();

                var sql = "SPU_Address";

                await conn.ExecuteAsync(sql,
                    new { AddressId = address.Id, Place = address.Place },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAddress(int addressId)
        {
            await using var conn = new SqlConnection(_databaseConnection);
            try
            {
                await conn.OpenAsync();

                var sql = "SPD_Address";

                await conn.ExecuteAsync(sql,
                    new { AddressId = addressId },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            await using var conn = new SqlConnection(_databaseConnection);
            try
            {
                await conn.OpenAsync();

                var sql = "SPS_AddressById";

                var addressDictionary = new Dictionary<int, Address>();

                var result = await conn.QueryAsync<Address, Customer, User, Address>(
                    sql,
                    (address, customer, user) =>
                    {
                        if (!addressDictionary.TryGetValue(address.Id, out var addressEntry))
                        {
                            addressEntry = address;
                            addressEntry.Customer = customer ?? new Customer();
                            addressEntry.Customer.Users = new List<User>();
                            addressDictionary.Add(address.Id, addressEntry);
                        }

                        if (user != null)
                        {
                            addressEntry.Customer.Users.Add(user);
                        }

                        return addressEntry;
                    },
                    new { AddressId = addressId },
                    commandType: System.Data.CommandType.StoredProcedure,
                    splitOn: "Id"
                );

                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Address>> GetAddressesByCustomerId(int customerId)
        {
            await using var conn = new SqlConnection(_databaseConnection);
            try
            {
                await conn.OpenAsync();

                var sql = "SPS_Address";

                var list = await conn.QueryAsync<Address>(
                    sql,
                    new { CustomerId = customerId },
                    commandType: System.Data.CommandType.StoredProcedure);

                return list?.ToList() ?? new List<Address>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
