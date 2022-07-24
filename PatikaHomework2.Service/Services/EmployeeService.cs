using Microsoft.EntityFrameworkCore;
using PatikaHomework2.Data.Context;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaHomework2.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EfContext _efContext;

        public EmployeeService(EfContext EfContext)
        {
            _efContext = EfContext;
        }

        public async Task<Employee> Add(Employee entity)
        {
            try
            {
                _efContext.Employee.AddAsync(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            
        }

        public async Task<string> Delete(int id)
        {
            var employee = _efContext.Employee.SingleOrDefault(x => x.Id == id);
            if (employee == null)
                return null;
            try
            {
                _efContext.Employee.Remove(employee);
                _efContext.SaveChangesAsync();
                return "Success";

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetById(int id)
        {
            return  _efContext.Employee.SingleOrDefault(x => x.Id == id);
            
        }

        public async Task<Employee> Update(Employee entity)
        {
            try
            {
                _efContext.Employee.Update(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
          
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _efContext.Set<Employee>().AsNoTracking().ToListAsync(); 
           
        }


    }
}
