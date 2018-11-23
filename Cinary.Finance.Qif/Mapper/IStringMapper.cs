using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif.Mapper
{
    interface IStringMapper<T> where T : ITransactionEntry
    {
        //void addAttribute(char key, string propertyName);
        //TransactionAttribute getAttribute(char key);
        //void Map(string datastring, ref IQifTransaction data);
    }
}
