using FinTracker.Core.Models;
using System.Text.Json;

namespace FinTracker.Core.Repositories
{
    public class ExpenseRepository
    {
        private string _filePath;
        private List<Expense> _expenses;
        private int _nextId;

        public ExpenseRepository(string filePath)
        {
            _filePath = filePath;
            _expenses = Load();
            _nextId = _expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1;
        }

        private List<Expense> Load()
        {
            if (!File.Exists(_filePath))
                return new List<Expense>();
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Expense>>(json)
                    ?? new List<Expense>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not load data: {ex.Message}");
                return new List<Expense>();
            }
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(_expenses, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, json);
        }

        public void Add(Expense expense)
        {
            expense.Id = _nextId++;
            _expenses.Add(expense);
            Save();
        }

        public List<Expense> GetAll()
        {
            return _expenses.ToList();
        }

        public bool Delete(int id)
        {
            Expense? expense = _expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null) return false;
            _expenses.Remove(expense);
            Save();
            return true;
        }
    }
}