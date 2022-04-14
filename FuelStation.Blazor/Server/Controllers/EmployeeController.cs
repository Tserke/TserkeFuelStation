using FuelStation.Blazor.Shared;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FuelStation.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeController: ControllerBase
    {

        private readonly IEntityRepo<Employee> _employeeRepo;
        public EmployeeController(IEntityRepo<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeListViewModel>> Get()
        {
            var result = await _employeeRepo.GetAllAsync();
            return result.Select(employee => new EmployeeListViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                HireDateStart=employee.HireDateStart,
                HireDateEnd=employee.HireDateEnd,
                SallaryPerMonth=employee.SallaryPerMonth,
                EmployeeType=employee.EmployeeType,

            });
        }
        [HttpGet("{id}")]
        public async Task<EmployeeEditListViewModel> Get(int id)
        {
            EmployeeEditListViewModel model = new();
            if (id != 0)
            {
                var existing = await _employeeRepo.GetByIdAsync(id);
                model.Id = existing.Id;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.HireDateStart=existing.HireDateStart;
                model.HireDateEnd=existing.HireDateEnd;
                model.SallaryPerMonth=existing.SallaryPerMonth;
                model.EmployeeType = existing.EmployeeType;
            }
            return model;
        }
        [HttpPost]
        public async Task Post(EmployeeEditListViewModel employee)
        {
            if(employee.HireDateStart<= DateTime.Now)
            {
                //TODO: needs a message to the user that he inputed a wrong date
                //("Time travel is not possible so you can not hire a person before now", nameof(employee.HireDateStart));
                return ;
            }
            //TODO: needs a way to disable the HireDateEnd field
            var newEmployee = new Employee
            {
                Name = employee.Name,
                Surname = employee.Surname,
                HireDateStart = (DateTime)employee.HireDateStart,
                HireDateEnd = employee.HireDateEnd,
                SallaryPerMonth = employee.SallaryPerMonth,
                EmployeeType = employee.EmployeeType,
            };
            await _employeeRepo.AddAsync(newEmployee);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employeeRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(EmployeeEditListViewModel employee)
        {
            var itemToUpdate = await _employeeRepo.GetByIdAsync(employee.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Name = employee.Name;
            itemToUpdate.Surname = employee.Surname;
            itemToUpdate.HireDateEnd=employee.HireDateEnd;
            itemToUpdate.SallaryPerMonth= employee.SallaryPerMonth;
            itemToUpdate.EmployeeType = employee.EmployeeType;

            await _employeeRepo.UpdateAsync(employee.Id, itemToUpdate);
            return Ok();
        }

    }
}
