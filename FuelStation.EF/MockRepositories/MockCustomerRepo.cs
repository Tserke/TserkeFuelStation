using FuelStation.EF.Context;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.MockRepositories
{
    public class MockCustomerRepo : IEntityRepo<Customer>
    {
        private List<Customer> _customers = new List<Customer> 
        {
            new Customer() { Id=1 , Name= "Dimitris", Surname="Tserke", CardNumber="A123456789"},
            new Customer() { Id=2 , Name="James",Surname="Tserke", CardNumber="A987654321"},
            new Customer(){ Id=3 , Name="Makis",Surname ="Tserke", CardNumber ="A123456987"}
        };

        public Task AddAsync(Customer entity)
        {

            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return Task.FromResult(_customers.AsEnumerable());
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return Task.FromResult(_customers.SingleOrDefault(customer => customer.Id == id));
        }

        public Task UpdateAsync(int id,Customer entity)
        {
            UpdateLogic(id,entity);
            return Task.CompletedTask;
        }





        private void AddLogic(Customer entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var lasId = _customers.OrderBy(Customer => Customer.Id).Last().Id;
            entity.Id = ++lasId;
            _customers.Add(entity);
        }


        private void DeleteLogic(int id)
        {
            var dbCustomer = _customers.SingleOrDefault(customer => customer.Id == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _customers.Remove(dbCustomer);
        }

        private void UpdateLogic(int id, Customer entity)
        {
            var dbCustomer = _customers.SingleOrDefault(customer => customer.Id == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbCustomer.Name = entity.Name;
            dbCustomer.Surname = entity.Surname;
            dbCustomer.CardNumber = entity.CardNumber;

        }
    }
}

