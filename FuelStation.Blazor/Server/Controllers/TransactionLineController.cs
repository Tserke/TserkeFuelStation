using FuelStation.Blazor.Shared;
using FuelStation.EF.Repositories;
using FuelStation.Model;
using Microsoft.AspNetCore.Mvc;

namespace FuelStation.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TransactionLineController : ControllerBase
    {
        private readonly IEntityRepo<TransactionLine> _transactionLineRepo;

        public TransactionLineController(IEntityRepo<TransactionLine> transactionLineRepo)
        {
            _transactionLineRepo = transactionLineRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<TransactionLineListViewModel>> Get()
        {
            var result = await _transactionLineRepo.GetAllAsync();
            return result.Select(transactionLine => new TransactionLineListViewModel
            {
                Id = transactionLine.Id,
                TransactionId = transactionLine.TransactionId,
                ItemId = transactionLine.ItemId,
                Quantity = transactionLine.Quantity,
                ItemPrice = transactionLine.ItemPrice,
                NetValue = transactionLine.NetValue,
                DiscountPercent = transactionLine.DiscountPercent,
                DiscountValue = transactionLine.DiscountValue,
                TotalValue = transactionLine.TotalValue,
            });
        }
        [HttpGet("{id}")]
        public async Task<TransactionLineListViewModel> Get(int id)
        {
            TransactionLineListViewModel model = new();
            if (id != 0)
            {
                var existing = await _transactionLineRepo.GetByIdAsync(id);
                model.Id = existing.Id;
                model.TransactionId = existing.TransactionId;
                model.ItemId = existing.ItemId;
                model.Quantity = existing.Quantity;
                model.ItemPrice = existing.ItemPrice;
                model.NetValue = existing.NetValue;
                model.DiscountPercent = existing.DiscountPercent;
                model.DiscountValue = existing.DiscountValue;
                model.TotalValue = existing.TotalValue;
            }
            return model;
        }
        [HttpPost]
        public async Task Post(TransactionLineListViewModel transactionLine)
        {
            var newTransactionLine = new TransactionLine
            {
                TransactionId = transactionLine.TransactionId,
                ItemId = transactionLine.ItemId,
                Quantity = transactionLine.Quantity,
                ItemPrice = transactionLine.ItemPrice,
                NetValue = transactionLine.NetValue,
                DiscountPercent = transactionLine.DiscountPercent,
                DiscountValue = transactionLine.DiscountValue,
                TotalValue = transactionLine.TotalValue,
            };
            await _transactionLineRepo.AddAsync(newTransactionLine);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _transactionLineRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(TransactionLineListViewModel transactionLine)
        {
            var transactionLineToUpdate = await _transactionLineRepo.GetByIdAsync(transactionLine.Id);
            if (transactionLineToUpdate == null) return NotFound();
            transactionLineToUpdate.TransactionId = transactionLine.TransactionId;
            transactionLineToUpdate.ItemId = transactionLine.ItemId;
            transactionLineToUpdate.Quantity = transactionLine.Quantity;
            transactionLineToUpdate.ItemPrice = transactionLine.ItemPrice;
            transactionLineToUpdate.NetValue = transactionLine.NetValue;
            transactionLineToUpdate.DiscountPercent = transactionLine.DiscountPercent;
            transactionLineToUpdate.DiscountValue = transactionLine.DiscountValue;
            transactionLineToUpdate.TotalValue = transactionLine.TotalValue;

            await _transactionLineRepo.UpdateAsync(transactionLine.Id, transactionLineToUpdate);
            return Ok();
        }
    }
}
