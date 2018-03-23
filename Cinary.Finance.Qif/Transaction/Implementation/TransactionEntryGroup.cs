namespace Cinary.Finance.Qif.Transaction
{
    public class TransactionEntryGroup : AbstractTransactionEntryGroup
    {
        public override string Title { get; set; }
        public override string Detail { get; set; }
        public override char ShortName { get; set; }
    }
}
