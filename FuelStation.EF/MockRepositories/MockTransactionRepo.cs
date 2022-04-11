using FuelStation.EF.Repositories;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.MockRepositories
{
    public class MockTransactionRepo : IEntityRepo<Transaction>
    {
        private List<Transaction> _transactions=new List<Transaction>
        {
            new Transaction(){Id=1,Date=DateTime.Now,EmployeeId=2,CustomerId=1, PaymentMethod = PaymentMethodEnum.Cash, TotalValue=50},
            new Transaction(){Id=2,Date=DateTime.Now,EmployeeId=3,CustomerId=1, PaymentMethod= PaymentMethodEnum.CreditCard, TotalValue=20},
            new Transaction(){Id=3,Date=DateTime.Now,EmployeeId=1,CustomerId=2, PaymentMethod = PaymentMethodEnum.Cash, TotalValue=15},
            new Transaction(){Id=4,Date=DateTime.Now,EmployeeId=1, CustomerId=3, PaymentMethod = PaymentMethodEnum.CreditCard,TotalValue=30},
        };

        public Task AddAsync(Transaction entity)
        {

            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return Task.FromResult(_transactions.AsEnumerable());
        }

        public Task<Transaction?> GetByIdAsync(int id)
        {
            return Task.FromResult(_transactions.SingleOrDefault(transaction => transaction.Id == id));
        }

        public Task UpdateAsync(int id, Transaction entity)
        {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }





        private void AddLogic(Transaction entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var lasId = _transactions.OrderBy(transaction => transaction.Id).Last().Id;
            entity.Id = ++lasId;
            _transactions.Add(entity);
        }


        private void DeleteLogic(int id)
        {
            var dbTransaction = _transactions.SingleOrDefault(transaction => transaction.Id == id);
            if (dbTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _transactions.Remove(dbTransaction);
        }

        private void UpdateLogic(int id, Transaction entity)
        {
            var dbTransaction = _transactions.SingleOrDefault(transaction => transaction.Id == id);
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
