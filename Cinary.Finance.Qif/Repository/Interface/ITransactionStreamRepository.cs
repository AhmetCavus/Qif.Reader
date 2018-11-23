//
//  IModifier.cs
//
//  Author:
//       ahc <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus
using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    /// <summary>
    /// This is an interface for the repository pattern to mark repository implementations. 
    /// </summary>
    public interface ITransactionStreamRepository
    {
        Task<IList<ITransactionEntry>> ResolveAsync(string id, System.IO.Stream resource);
        IList<ITransactionEntry> Resolve(string id, System.IO.Stream resource);
    }

}
