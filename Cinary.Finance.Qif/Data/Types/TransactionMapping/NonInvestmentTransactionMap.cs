using Cinary.Finance.Qif.Data.TransactionTypes;

namespace Cinary.Finance.Qif.Data.TransactionMapping
{
    internal class NonInvestmentTransactionMap : TransactionMap<NonInvestmentTransaction>
    {
        public NonInvestmentTransactionMap()
        {
            this.AddAttribute('D', "Date");
            this.AddAttribute('T', "Amount");
            this.AddAttribute('C', "IsCleared");
            this.AddAttribute('N', "Num");
            this.AddAttribute('P', "PayeeLines");
            this.AddAttribute('M', "Memo");
            this.AddAttribute('A', "AddressLines");
            this.AddAttribute('L', "Category");
            this.AddAttribute('S', "SplitCategory");
            this.AddAttribute('E', "SplitMemo");
            this.AddAttribute('$', "SplitAmount");
        }
    }
}
