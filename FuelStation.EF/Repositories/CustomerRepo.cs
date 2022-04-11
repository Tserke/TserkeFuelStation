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
    public class CustomerRepo : IEntityRepo<Customer>
    {

        private readonly FuelStationContext _context;



        public CustomerRepo(FuelStationContext dbContext)
        {
            _context = dbContext;
        }


        public async Task AddAsync(Customer entity)
        {

            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task UpdateAsync(int id, Customer entity)
        {
            UpdateLogic(id, entity, _context);
            await _context.SaveChangesAsync();
        }





        private void AddLogic(Customer entity, FuelStationContext context)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Customers.Add(entity);
        }


        private void DeleteLogic(int id, FuelStationContext context)
        {
            var dbCustomer = context.Customers.SingleOrDefault(customer => customer.Id == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Customers.Remove(dbCustomer);
        }

        private void UpdateLogic(int id, Customer entity, FuelStationContext context)
        {
            var dbCustomer = context.Customers.SingleOrDefault(customer => customer.Id == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbCustomer.Name = entity.Name;
            dbCustomer.Surname = entity.Surname;
            dbCustomer.CardNumber = entity.CardNumber;

        }
    }

}

