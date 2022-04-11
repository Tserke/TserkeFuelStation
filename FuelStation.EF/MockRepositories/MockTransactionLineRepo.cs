using FuelStation.EF.Repositories;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.MockRepositories
{
    public class MockTransactionLineRepo : IEntityRepo<TransactionLine>
    {
        private List<TransactionLine> _transactionLines = new List<TransactionLine>
        {
            new TransactionLine(){Id = 1, TransactionId = 1,ItemId = 1, Quantity=25, DiscountPercent=10 , DiscountValue=5,NetValue=50, ItemPrice=2, TotalValue=45},
            new TransactionLine(){Id = 2, TransactionId = 2,ItemId = 2,Quantity=25, DiscountPercent=0 , DiscountValue=0,NetValue=50,ItemPrice=2,TotalValue=50}
        };
        public Task AddAsync(TransactionLine entity)
        {

            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TransactionLine>> GetAllAsync()
        {
            return Task.FromResult(_transactionLines.AsEnumerable());
        }

        public Task<TransactionLine?> GetByIdAsync(int id)
        {
            return Task.FromResult(_transactionLines.SingleOrDefault(transactionLine => transactionLine.Id == id));
        }

        public Task UpdateAsync(int id, TransactionLine entity)
        {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }





        private void AddLogic(TransactionLine entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var lasId = _transactionLines.OrderBy(transactionLine => transactionLine.Id).Last().Id;
            entity.Id = ++lasId;
            _transactionLines.Add(entity);
        }


        private void DeleteLogic(int id)
        {
            var dbTransactionLines = _transactionLines.SingleOrDefault(transactionLine => transactionLine.Id == id);
            if (dbTransactionLines is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _transactionLines.Remove(dbTransactionLines);
        }

        private void UpdateLogic(int id, TransactionLine entity)
        {
            var dbTransactionLines = _transactionLines.SingleOrDefault(transactionLine => transactionLine.Id == id);
            if (dbTransactionLines is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbTransactionLines.TransactionId = entity.TransactionId;
            dbTransactionLines.ItemId = entity.ItemId;
            dbTransactionLines.Quantity = entity.Quantity;
            dbTransactionLines.ItemPrice = entity.ItemPrice;
            dbTransactionLines.NetValue = entity.NetValue;
            dbTransactionLines.DiscountPercent = entity.DiscountPercent;
            dbTransactionLines.DiscountValue = entity.DiscountValue;
            dbTransactionLines.TotalValue = entity.TotalValue;

        }
    }
}
