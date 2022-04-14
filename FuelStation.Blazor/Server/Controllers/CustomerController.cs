using FuelStation.Blazor.Shared;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FuelStation.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IEntityRepo<Customer> _customerRepo;
        public CustomerController(IEntityRepo<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerListViewModel>> Get()
        {
            var result = await _customerRepo.GetAllAsync();
            return result.Select(customer => new CustomerListViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                CardNumber = customer.CardNumber,

            });
        }
        [HttpGet("{id}")]
        public async Task<CustomerEditListViewModel> Get(int id)
        {
            CustomerEditListViewModel model = new();
            if (id != 0)
            {
                var existing = await _customerRepo.GetByIdAsync(id);
                model.Id = existing.Id;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.CardNumber = existing.CardNumber;
            }
            return model;
        }
        [HttpPost]
        public async Task Post(CustomerEditListViewModel customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name,
                Surname = customer.Surname,
                CardNumber = customer.CardNumber
            };
            await _customerRepo.AddAsync(newCustomer);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _customerRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CustomerEditListViewModel customer)
        {
            var itemToUpdate = await _customerRepo.GetByIdAsync(customer.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Name = customer.Name;
            itemToUpdate.Surname = customer.Surname;
            itemToUpdate.CardNumber = customer.CardNumber;

            await _customerRepo.UpdateAsync(customer.Id, itemToUpdate);
            return Ok();
        }
    }
}
