using FuelStation.EF.Repositories;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.MockRepositories
{
    public class MockItemRepo : IEntityRepo<Item>
    {
        private List<Item> _items = new List<Item>
        {
            new Item(){Id = 1, Code = "00001", Description="Petrol",ItemType=ItemTypeEnum.Fuel,Price=1.987,Cost=1.275},
            new Item(){Id = 2, Code = "00002", Description="Diesel", ItemType=ItemTypeEnum.Fuel,Price=1.687, Cost=0.987},
            new Item(){Id = 3, Code = "00003", Description="Engine Oil",ItemType=ItemTypeEnum.Product, Price= 25.50,Cost=12.5},
            new Item(){Id = 4, Code = "00004", Description="Oil Change", ItemType = ItemTypeEnum.Service, Price = 10,Cost=3.50}
        };

        public Task AddAsync(Item entity)
        {

            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<Item?> GetByIdAsync(int id)
        {
            return Task.FromResult(_items.SingleOrDefault(item => item.Id == id));
        }

        public Task UpdateAsync(int id, Item entity)
        {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }





        private void AddLogic(Item entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var lasId = _items.OrderBy(item => item.Id).Last().Id;
            entity.Id = ++lasId;
            _items.Add(entity);
        }


        private void DeleteLogic(int id)
        {
            var dbItem = _items.SingleOrDefault(item => item.Id == id);
            if (dbItem is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _items.Remove(dbItem);
        }

        private void UpdateLogic(int id, Item entity)
        {
            var dbItem = _items.SingleOrDefault(item => item.Id == id);
            if (dbItem is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbItem.Code = entity.Code;
            dbItem.Description = entity.Description;
            dbItem.ItemType = entity.ItemType;
            dbItem.Price = entity.Price;
            dbItem.Cost = entity.Cost;

        }
    }
}
