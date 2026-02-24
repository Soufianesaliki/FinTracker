namespace FinTracker
{
    class Program
    {
        static ExpenseService _service = new ExpenseService(
            new ExpenseRepository("expense.json")
        );

        static void Main(string[] args)
        {
            Console.WriteLine("=== Expense Tracker ===\n");

            while (true)
            {
                PrintMenu();
                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": AddExpense(); break;
                    case "2": ViewAll(); break;
                    case "3": ViewByCategory(); break;
                    case "4": DeleteExpense(); break;
                    case "5": _service.PrintSummary(); break;
                    case "6": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid option, try again."); break;
                }
                Console.WriteLine();                
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("1. Add expense");
            Console.WriteLine("2. View all expenses");
            Console.WriteLine("3. View by category");
            Console.WriteLine("4. Delete expense");
            Console.WriteLine("5. Summary");
            Console.WriteLine("6. Exit");
            Console.Write("Choice: ");
        }

        static void AddExpense()
        {
            Console.Write("Name : ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Amount : ");
            if (!double.TryParse(Console.ReadLine(), out double amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            Console.Write("Category : ");
            string category = Console.ReadLine() ?? "";

            try
            {
                _service.AddExpense(name, amount, category);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        static void ViewAll()
        {
            List<Expense> expenses = _service.GetAll();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses yet.");
                return;
            }

            foreach (Expense e in expenses)
            {
                Console.WriteLine(e);
            }
        }

        static void ViewByCategory()
        {
            Console.Write("Category : ");
            string category = Console.ReadLine() ?? "";

            List<Expense> expenses = _service.GetByCategory(category);

            if (!expenses.Any())
            {
                Console.WriteLine($"No expenses in category '{category}'.");
                return;
            }

            foreach (Expense e in expenses)
            {
                Console.WriteLine(e);
            }
        }

        static void DeleteExpense()
        {
            Console.Write("Expense ID to delete : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            bool deleted = _service.DeleteExpense(id);
            Console.WriteLine(deleted ? "Deleted." : "Expense not found.");
        }
    }
}