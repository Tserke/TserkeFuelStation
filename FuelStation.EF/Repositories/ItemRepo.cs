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
    public class ItemRepo : IEntityRepo<Item>
    {
        private readonly FuelStationContext _context;



        public ItemRepo(FuelStationContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(Item entity)
        {
            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items.SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task UpdateAsync(int id, Item entity)
        {
            UpdateLogic(id, entity, _context);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(Item entity, FuelStationContext context)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Items.Add(entity);
        }


        private void DeleteLogic(int id, FuelStationContext context)
        {
            var dbItem = context.Items.SingleOrDefault(item => item.Id == id);
            if (dbItem is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Items.Remove(dbItem);
        }

        private void UpdateLogic(int id, Item entity, FuelStationContext context)
        {
            var dbItem = context.Items.SingleOrDefault(item => item.Id == id);
            if (dbItem is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbItem.Code = entity.Code;
            dbItem.Description=entity.Description;
            dbItem.ItemType = entity.ItemType;
            dbItem.Price = entity.Price;
            dbItem.Cost = entity.Cost;
        }
    }
}
