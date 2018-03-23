using System.Collections.ObjectModel;

namespace Cinary.Finance.Qif.Transaction
{
    public abstract class AbstractTransactionEntryGroup : ObservableCollection<ITransactionEntry>
    {
        public abstract string Title { get; set; }
        public abstract string Detail { get; set; }
        public abstract char ShortName { get; set; }
    }
}
