//
//  IModifier.cs
//
//  Author:
//       ahc <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus
using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Data.Description;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    /// <summary>
    /// This is an interface for the repository pattern to mark repository implementations. 
    /// </summary>
    public interface ITransactionRepository<TTarget> : ITransactionRepository where TTarget : ITransaction
	{
        Task<TTarget> ResolveAsync(IDataDescription description = null);
        TTarget Resolve(IDataDescription description = null);
    }

    /// <summary>
    /// This is an interface for the repository pattern to mark repository implementations. 
    /// </summary>
    public interface ITransactionRepository
    {
        Task<ITransaction> ResolveAsync(string resource);
        ITransaction Resolve(string resource);
        void SetTransactionReader(ITransactionReader context);
    }

}
