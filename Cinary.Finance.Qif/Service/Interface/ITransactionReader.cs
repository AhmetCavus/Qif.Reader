using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionReader
    {
        Task<IList<ITransactionEntry>> ReadFromResourceAsync<TType>(string resource) where TType : ITransactionEntry;

        IList<ITransactionEntry> ReadFromResource<TType>(string resource) where TType : ITransactionEntry;

        Task<IList<ITransactionEntry>> ReadFromFileAsync<TType>(string resource) where TType : ITransactionEntry;

        IList<ITransactionEntry> ReadFromFile<TType>(string resource) where TType : ITransactionEntry;

        Task<IList<ITransactionEntry>> ReadAsync<TType>(System.IO.Stream resource) where TType : ITransactionEntry;

        IList<ITransactionEntry> Read<TType>(System.IO.Stream resource) where TType : ITransactionEntry;
    }
}
