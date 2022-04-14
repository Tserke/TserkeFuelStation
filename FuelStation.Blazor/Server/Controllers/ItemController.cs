using FuelStation.Blazor.Shared;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FuelStation.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IEntityRepo<Item> _itemRepo;

        public ItemController(IEntityRepo<Item> itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemListViewModel>> Get()
        {
            var result = await _itemRepo.GetAllAsync();
            return result.Select(item => new ItemListViewModel
            {
                Id = item.Id,
                Code = item.Code,
                Description = item.Description,
                ItemType = item.ItemType,
                Price = item.Price,
                Cost = item.Cost,
            });
        }
        [HttpGet("{id}")]
        public async Task<ItemEditListViewModel> Get(int id)
        {
            ItemEditListViewModel model = new();
            if (id != 0)
            {
                var existing = await _itemRepo.GetByIdAsync(id);
                model.Id = existing.Id;
                model.Code = existing.Code;
                model.Description = existing.Description;
                model.ItemType = existing.ItemType;
                model.Price= existing.Price;
                model.Cost = existing.Cost;
            }
            return model;
        }
        [HttpPost]
        public async Task Post(ItemEditListViewModel item)
        {

            var newItem = new Item
            {
                Code= item.Code,
                Description= item.Description,
                ItemType= item.ItemType,
                Price= item.Price,
                Cost= item.Cost,
            };
            await _itemRepo.AddAsync(newItem);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _itemRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(ItemEditListViewModel item)
        {
            var itemToUpdate = await _itemRepo.GetByIdAsync(item.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Code = itemToUpdate.Code;
            itemToUpdate.Description = itemToUpdate.Description;
            itemToUpdate.ItemType= itemToUpdate.ItemType;
            itemToUpdate.Price= itemToUpdate.Price;
            itemToUpdate.Cost= itemToUpdate.Cost;

            await _itemRepo.UpdateAsync(item.Id, itemToUpdate);
            return Ok();
        }

    }
}
