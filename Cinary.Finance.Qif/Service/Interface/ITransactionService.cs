//
//  IDataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System;
using System.IO;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionService : IDisposable
    {
        //Task<ITransactionDetail> QueryAsync<TTarget>(IDataDescription description = null) where TTarget : class, ITransaction;

        //ITransactionDetail Query<TTarget>(IDataDescription description = null) where TTarget : class, ITransaction;

        Task<ITransactionDetail> QueryAsync<TTarget>(string id, Stream resource) where TTarget : class, ITransaction;

        ITransactionDetail Query<TTarget>(string id, Stream resource) where TTarget : class, ITransaction;

        Task<ITransactionDetail> QueryFromFileAsync<TTarget>(string path) where TTarget : class, ITransaction;

        ITransactionDetail QueryFromFile<TTarget>(string path) where TTarget : class, ITransaction;

        Task<ITransactionDetail> QueryFromResourceAsync<TTarget>(string resource) where TTarget : class, ITransaction;

        ITransactionDetail QueryFromResource<TTarget>(string resource) where TTarget : class, ITransaction;

    }
}
