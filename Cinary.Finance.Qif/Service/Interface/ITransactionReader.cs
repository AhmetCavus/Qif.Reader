using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Data.Description;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionReader
    {
        Task<IList<TType>> ReadFromResourceAsync<TType>(string resource) where TType : ITransaction;

        IList<TType> ReadFromResource<TType>(string resource) where TType : ITransaction;

        Task<IList<TType>> ReadAsync<TType>(string resource) where TType : ITransaction;

        IList<TType> Read<TType>(string resource) where TType : ITransaction;
    }
}
