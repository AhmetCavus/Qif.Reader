namespace Cinary.Finance.Qif.NonTransactionTypes
{
    public class CategoryInfo
    {
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTaxRelated { get; set; }
        public bool IsIncomeCategory { get; set; }
        public bool IsExpenseCategory { get; set; }
        public decimal BudgetAmount { get; set; }
        public string TaxSheduleInformation { get; set; }
    }
}
