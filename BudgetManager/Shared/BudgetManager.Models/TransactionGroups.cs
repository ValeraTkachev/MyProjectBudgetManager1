namespace BudgetManager.Models
{
    using System.Collections.Generic;

    public class TransactionGroups
    {
        public string Title { get; set; }

        public List<Transaction> Items { get; set; }
    }
}
