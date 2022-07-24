
using Dapper;
using PatikaHomework2.Data.Context;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaHomework2.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly DapperContext dapperDbContext;

        public CountryService(DapperContext dapperDbContext) : base()
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {

            var sql = "SELECT * FROM department";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Country>(sql);
                return result;
            }

        }

        public async Task<Country> GetById(int id)
        {
            var query = "SELECT * FROM country WHERE Id = @Id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Country>(query, new { id });
                return result;
            }
        }

        public async Task<Country> Add(Country entity)
        { 
            var query = "INSERT INTO country(countryname, continent, currency)" +
                "VALUES(@CountryName, @Continent, @Currency)";

            var parameters = new DynamicParameters();
            parameters.Add("CountryName", entity.CountryName, DbType.String);
            parameters.Add("Continent", entity.Continent, DbType.String);
            parameters.Add("Currency", entity.Currency, DbType.String);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result =await connection.ExecuteAsync(query, parameters);

                if (result == 0)
                    return null;
                return entity;
            }
        }

        public async Task<string> Delete(int id)
        {
            var query = "DELETE FROM country WHERE Id = @id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, new { id });
                if (result == 0)
                    return null;

                return "Success";
            }
        }


        public async Task<Country> Update(Country entity)
        {
            throw new NotImplementedException();
        }

        //Task<IEnumerable<Country>> IGenericService<Country>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
