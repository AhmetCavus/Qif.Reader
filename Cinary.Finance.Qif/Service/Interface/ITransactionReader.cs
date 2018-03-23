using Cinary.Finance.Qif.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionReader
    {
        Task<IList<TType>> ReadFromResourceAsync<TType>(string resource) where TType : ITransaction;

        IList<TType> ReadFromResource<TType>(string resource) where TType : ITransaction;

        Task<IList<TType>> ReadFromFileAsync<TType>(string resource) where TType : ITransaction;

        IList<TType> ReadFromFile<TType>(string resource) where TType : ITransaction;

        Task<IList<TType>> ReadAsync<TType>(System.IO.Stream resource) where TType : ITransaction;

        IList<TType> Read<TType>(System.IO.Stream resource) where TType : ITransaction;
    }
}
