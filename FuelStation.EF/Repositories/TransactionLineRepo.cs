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
    public class TransactionLineRepo : IEntityRepo<TransactionLine>
    {
        private readonly FuelStationContext _context;

        public TransactionLineRepo(FuelStationContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(TransactionLine entity)
        {
            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionLine>> GetAllAsync()
        {
            return await _context.TransactionLines.ToListAsync();
        }

        public async Task<TransactionLine?> GetByIdAsync(int id)
        {
            return await _context.TransactionLines.SingleOrDefaultAsync(transactionLine => transactionLine.Id == id);
        }

        public async Task UpdateAsync(int id, TransactionLine entity)
        {
            UpdateLogic(id, entity, _context);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(TransactionLine entity, FuelStationContext context)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));
            context.TransactionLines.Add(entity);
        }
        private void DeleteLogic(int id, FuelStationContext context)
        {
            var dbTransactionLines = context.TransactionLines.SingleOrDefault(transactionLine => transactionLine.Id == id);
            if (dbTransactionLines is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            context.TransactionLines.Remove(dbTransactionLines);
        }
        private void UpdateLogic(int id, TransactionLine entity, FuelStationContext context)
        {
            var dbTransactionLines = context.TransactionLines.SingleOrDefault(transactionLine => transactionLine.Id == id);
            if (dbTransactionLines is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbTransactionLines.TransactionId = entity.TransactionId;
            dbTransactionLines.ItemId = entity.ItemId;
            dbTransactionLines.Quantity=entity.Quantity;
            dbTransactionLines.ItemPrice = entity.ItemPrice;
            dbTransactionLines.NetValue = entity.NetValue;
            dbTransactionLines.DiscountPercent = entity.DiscountPercent;
            dbTransactionLines.DiscountValue=entity.DiscountValue;
            dbTransactionLines.TotalValue = entity.TotalValue;
        }
    }
}
