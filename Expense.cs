namespace FinTracker
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }

        public Expense(string name, double amount, string category)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Id}] {Date:yyyy-mm-dd} | {Category, -15} | {Name, -20} | {Amount:C}";
        }
    }
}