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
    public class TransactionRepo : IEntityRepo<Transaction>
    {
        private readonly FuelStationContext _context;

        public TransactionRepo(FuelStationContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(Transaction entity)
        {
            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.SingleOrDefaultAsync(transaction => transaction.Id == id);
        }

        public async Task UpdateAsync(int id, Transaction entity)
        {
            UpdateLogic(id, entity, _context);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(Transaction entity, FuelStationContext context)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));
            context.Transactions.Add(entity);
        }
        private void DeleteLogic(int id, FuelStationContext context)
        {
            var dbTransaction = context.Transactions.SingleOrDefault(transaction => transaction.Id == id);
            if (dbTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            context.Transactions.Remove(dbTransaction);
        }
        private void UpdateLogic(int id, Transaction entity, FuelStationContext context)
        {
            var dbTransaction = context.Transactions.SingleOrDefault(transaction => transaction.Id == id);
            if (dbTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbTransaction.Date = entity.Date;
            dbTransaction.EmployeeId = entity.EmployeeId;
            dbTransaction.CustomerId = entity.CustomerId;
            dbTransaction.PaymentMethod = entity.PaymentMethod;
            dbTransaction.TotalValue = entity.TotalValue;

        }
    }
}

