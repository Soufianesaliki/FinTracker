using Microsoft.AspNetCore.Mvc;
using FinTracker.API.Models;
using FinTracker.Core.Models;
using FinTracker.Core.Services;

namespace FinTracker.API.Controllers
{
    [ApiController]
    [Route("expenses")]
    public class ExpenseController : ControllerBase
    {
        private ExpenseService _service;

        public ExpenseController(ExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Expense> expenses = _service.GetAll();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Expense? expense = _service.GetAll()
                .FirstOrDefault(e => e.Id == id);
            if (expense == null)
                return NotFound($"Expense with ID {id} not found");
            return Ok(expense);
        }

        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            List<Expense> expenses = _service.GetByCategory(category);
            return Ok(expenses);
        }
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            List<Expense> all = _service.GetAll();
            if (!all.Any())
                return Ok(new { message = "No expenses recorded yet" });
            
            var summary = new
            {
                TotalExpenses = all.Count,
                TotalSpent = all.Sum(e => e.Amount),
                AverageExpense = all.Average(e => e.Amount),
                MostExpensive = all.MaxBy(e => e.Amount)?.Name,
                TopCategory = all
                    .GroupBy(e => e.Category)
                    .OrderByDescending(g => g.Sum(e => e.Amount))
                    .First().Key
            };
            return Ok(summary);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddExpenseRequest request)
        {
            try
            {
                _service.AddExpense(request.Name, request.Amount, request.Category);
                return Created("", new { message = "Expense added successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _service.DeleteExpense(id);
            if (!deleted)
                return NotFound($"Expense with ID {id} not found");
            return Ok(new { message = $"Expense {id} deleted"});
        }
    }
}