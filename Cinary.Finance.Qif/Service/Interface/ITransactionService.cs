//
//  IDataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Data.Description;

namespace Cinary.Finance.Qif.Service
{
    public interface ITransactionService : IDisposable
    {
        Task<TTarget> QueryAsync<TTarget>(IDataDescription description = null) where TTarget : class, ITransaction;

        Task<TTarget> QueryAsync<TTarget>(string resource) where TTarget : class, ITransaction;

        TTarget Query<TTarget>(IDataDescription description = null) where TTarget : class, ITransaction;

        TTarget Query<TTarget>(string resource) where TTarget : class, ITransaction;
    }
}
