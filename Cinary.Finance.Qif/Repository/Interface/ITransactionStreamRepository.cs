//
//  IModifier.cs
//
//  Author:
//       ahc <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus
using Cinary.Finance.Qif.Transaction;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    /// <summary>
    /// This is an interface for the repository pattern to mark repository implementations. 
    /// </summary>
    public interface ITransactionStreamRepository
    {
        Task<ITransactionDetail> ResolveAsync(string id, System.IO.Stream resource);
        ITransactionDetail Resolve(string id, System.IO.Stream resource);
    }

}
