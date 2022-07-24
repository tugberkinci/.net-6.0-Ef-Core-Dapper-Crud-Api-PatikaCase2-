using Dapper;
using PatikaHomework2.Data.Context;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Service.IServices;
using System.Data;

namespace PatikaHomework2.Service.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly DapperContext dapperDbContext;

        public DepartmentService(DapperContext dapperDbContext) : base()
        {
            this.dapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {

            var sql = "SELECT * FROM department";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Department>(sql);
                return result;
            }
            
        }

        public async Task<Department> GetById(int id)
        {
            var query = "SELECT * FROM department WHERE id = @Id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Department>(query, new { id });
                return result;
            }
        }

        public async Task<Department> Add(Department entity)
        {
            var query = "INSERT INTO  department (deptname, countryid)"+
                " VALUES (@DeptName, @CountryId)";
            
            var parameters = new DynamicParameters();
            parameters.Add("DeptName", entity.DeptName, DbType.String);
            parameters.Add("CountryId", entity.CountryId, DbType.Int32);
            

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
                return entity;
            }
        }

        public async Task<string> Delete(int id)
        {
            var query = "DELETE FROM department WHERE Id = @id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, new { id});
                if(result == 0)
                return null;

                return "Success";
            }
        }

     
        public async Task<Department> Update(Department entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
