namespace FinTracker.API.Models
{
    public class AddExpenseRequest
    {
        public string Name { get; set; } = "";
        public double Amount { get; set; }
        public string Category { get; set; } = "";
    }
}