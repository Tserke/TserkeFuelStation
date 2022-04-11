using FuelStation.EF.Context;
using FuelStation.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.Repositories
{
    public class EmployeeRepo : IEntityRepo<Employee>
    {
        private readonly FuelStationContext _context;


        public EmployeeRepo(FuelStationContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task AddAsync(Employee entity)
        {


            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(_context, id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {

            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {

            return await _context.Employees.Where(employee => employee.Id == id).SingleOrDefaultAsync();
        }


        public async Task UpdateAsync(int id, Employee entity)
        {
            UpdateLogic(entity, _context, id);
            await _context.SaveChangesAsync(); ;
        }

        public void AddLogic(Employee entity, FuelStationContext context)
        {
            if (entity.Id != 0)
            {
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            }
            context.Employees.Add(entity);
        }
        private void DeleteLogic(FuelStationContext context, int id)
        {
            var foundEmployee = context.Employees.SingleOrDefault(employee => employee.Id == id);
            if (foundEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Employees.Remove(foundEmployee);
            context.SaveChangesAsync();
        }

        private void UpdateLogic(Employee entity, FuelStationContext context, int id)
        {
            var dbEmployee = context.Employees.SingleOrDefault(employee => employee.Id == id);
            if (dbEmployee is null)
                return;

            dbEmployee.Name = entity.Name;
            dbEmployee.Surname = entity.Surname;
            dbEmployee.HireDateStart = entity.HireDateStart;
            dbEmployee.HireDateEnd = entity.HireDateEnd;
            dbEmployee.SallaryPerMonth = entity.SallaryPerMonth;
            dbEmployee.EmployeeType = entity.EmployeeType;
        }

    }
}
