//
//  IDataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionService : IDisposable
    {
        Task<IList<ITransactionEntry>> QueryAsync<TTarget>(string id, Stream resource) where TTarget : class, ITransactionEntry;

        IList<ITransactionEntry> Query<TTarget>(string id, Stream resource) where TTarget : class, ITransactionEntry;

        Task<IList<ITransactionEntry>> QueryFromFileAsync<TTarget>(string path) where TTarget : class, ITransactionEntry;

        IList<ITransactionEntry> QueryFromFile<TTarget>(string path) where TTarget : class, ITransactionEntry;

        Task<IList<ITransactionEntry>> QueryFromResourceAsync<TTarget>(string resource) where TTarget : class, ITransactionEntry;

        IList<ITransactionEntry> QueryFromResource<TTarget>(string resource) where TTarget : class, ITransactionEntry;

    }
}
