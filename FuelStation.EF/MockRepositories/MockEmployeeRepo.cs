using FuelStation.EF.Repositories;
using FuelStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.MockRepositories
{
    public class MockEmployeeRepo : IEntityRepo<Employee>
    {
        private List<Employee> _employees = new List<Employee>
        {
            new Employee(){Id = 1, Name = "Takis", Surname = "Manageridis", HireDateStart = new DateTime(2021,04,10), HireDateEnd = new DateTime(2022,04,10), SallaryPerMonth=3000.99, EmployeeType = EmployeeTypeEnum.Manager },
            new Employee(){Id = 2, Name = "Akis", Surname = "Manageridis", HireDateStart = new DateTime(2022,04,10), HireDateEnd = null, SallaryPerMonth= 2000.90, EmployeeType =EmployeeTypeEnum.Manager },
            new Employee(){Id = 3, Name = "Sakis", Surname = "Staffakis", HireDateStart= new DateTime(2021,12,01), HireDateEnd=null, SallaryPerMonth = 650.50, EmployeeType= EmployeeTypeEnum.Staff},
            new Employee(){Id = 4, Name ="Maria", Surname = "Cashieropoulou", HireDateStart= new DateTime(2019,08,24), HireDateEnd=null, SallaryPerMonth = 900, EmployeeType=EmployeeTypeEnum.Cashier}
        };

        public Task AddAsync(Employee entity)
        {

            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return Task.FromResult(_employees.AsEnumerable());
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return Task.FromResult(_employees.SingleOrDefault(employee => employee.Id == id));
        }

        public Task UpdateAsync(int id, Employee entity)
        {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }





        private void AddLogic(Employee entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            var lasId = _employees.OrderBy(employee => employee.Id).Last().Id;
            entity.Id = ++lasId;
            _employees.Add(entity);
        }


        private void DeleteLogic(int id)
        {
            var dbEmployee = _employees.SingleOrDefault(employee => employee.Id == id);
            if (dbEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _employees.Remove(dbEmployee);
        }

        private void UpdateLogic(int id, Employee entity)
        {
            var dbEmployee = _employees.SingleOrDefault(employee => employee.Id == id);
            if (dbEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbEmployee.Name = entity.Name;
            dbEmployee.Surname = entity.Surname;
            dbEmployee.HireDateStart = entity.HireDateStart;
            dbEmployee.HireDateEnd = entity.HireDateEnd;
            dbEmployee.SallaryPerMonth = entity.SallaryPerMonth;
            dbEmployee.EmployeeType = entity.EmployeeType;

        }
    }
}
