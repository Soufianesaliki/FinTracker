using System.ComponentModel;

namespace FinTracker
{
    class ExpenseService
    {
        private ExpenseRepository _repository;

        public ExpenseService(ExpenseRepository repository)
        {
            _repository = repository;
        }

        public void AddExpense(string name, double amount, string category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Expense name cannot be empty");
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty");
            
            Expense expense = new Expense(name, amount, category);
            _repository.Add(expense);
            Console.WriteLine($"Added : {expense}");
        }

        public List<Expense> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Expense> GetByCategory(string category)
        {
            return _repository.GetAll()
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool DeleteExpense(int id)
        {
            return _repository.Delete(id);
        }

        public void PrintSummary()
        {
            List<Expense> all = _repository.GetAll();

            if (!all.Any())
            {
                Console.WriteLine("No expenses recorded yet.");
                return;
            }

            double total = all.Sum(a => a.Amount);
            double average = all.Average(a => a.Amount);
            Expense mostExpensive = all.MaxBy(a => a.Amount);
            string topCategory = all
                .GroupBy(c => c.Category)
                .OrderByDescending(t => t.Sum(a => a.Amount))
                .First()
                .Key;
            
            Console.WriteLine("=== Summary ===");
            Console.WriteLine($"Total expenses:     {all.Count}");
            Console.WriteLine($"Total spent:        {total:C}");
            Console.WriteLine($"Average expense:    {average:C}");
            Console.WriteLine($"Most expensive:     {mostExpensive.Name} ({mostExpensive.Amount:C})");
            Console.WriteLine($"Top category:       {topCategory}");
        }
    }
}