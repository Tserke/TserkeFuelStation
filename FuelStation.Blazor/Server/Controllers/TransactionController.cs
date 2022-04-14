using FuelStation.Blazor.Shared;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FuelStation.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IEntityRepo<Transaction> _transactionRepo;

        public TransactionController(IEntityRepo<Transaction> transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<TransactionListViewModel>> Get()
        {
            var result = await _transactionRepo.GetAllAsync();
            return result.Select(transaction => new TransactionListViewModel
            {
                Id = transaction.Id,
                Date = transaction.Date,
                EmployeeId = transaction.EmployeeId,
                CustomerId = transaction.CustomerId,
                PaymentMethod = transaction.PaymentMethod,
                TotalValue = transaction.TotalValue,
            });
        }
        [HttpGet("{id}")]
        public async Task<TransactionListViewModel> Get(int id)
        {
            TransactionListViewModel model = new();
            if (id != 0)
            {
                var existing = await _transactionRepo.GetByIdAsync(id);
                model.Id = existing.Id;
                model.Date = existing.Date;
                model.EmployeeId = existing.EmployeeId;
                model.CustomerId = existing.CustomerId;
                model.PaymentMethod = existing.PaymentMethod;
                model.TotalValue = existing.TotalValue;
            }
            return model;
        }
        [HttpPost]
        public async Task Post(TransactionListViewModel transactionRepo)
        {
            var newTransaction = new Transaction
            {
                Date = transactionRepo.Date,
                EmployeeId = transactionRepo.EmployeeId,
                CustomerId = transactionRepo.CustomerId,
                PaymentMethod = transactionRepo.PaymentMethod,
                TotalValue = transactionRepo.TotalValue,
            };
            await _transactionRepo.AddAsync(newTransaction);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _transactionRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(TransactionListViewModel transaction)
        {
            var transactionToUpdate = await _transactionRepo.GetByIdAsync(transaction.Id);
            if (transactionToUpdate == null) return NotFound();
            transactionToUpdate.Date = transactionToUpdate.Date;
            transactionToUpdate.EmployeeId = transactionToUpdate.EmployeeId;
            transactionToUpdate.CustomerId = transactionToUpdate.CustomerId;
            transactionToUpdate.PaymentMethod = transactionToUpdate.PaymentMethod;
            transactionToUpdate.TotalValue = transactionToUpdate.TotalValue;

            await _transactionRepo.UpdateAsync(transaction.Id, transactionToUpdate);
            return Ok();
        }
    }
}