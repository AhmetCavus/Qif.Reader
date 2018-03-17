//
//  DataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System.Threading.Tasks;
using Cinary.Finance.Qif.Data.Description;
using Cinary.Finance.Qif.Repository;
using Cinary.Finance.Qif.Service;

namespace Cinary.Finance.Qif.Data
{
    public class QifService : ITransactionService
    {
        protected ITransactionReader _qifReader = new ITransactionReader();
        protected ITransactionRepositoryContainer _container = new QifRepositoryContainer();

        public async Task<TTarget> QueryAsync<TTarget>(IDataDescription description = null)
        where TTarget : class, ITransaction
        {
            var repo = _container.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveAsync(description);
            return target;
        }

        public TTarget Query<TTarget>(IDataDescription description = null)
        where TTarget : class, ITransaction
        {
            var repo = _container.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target =  repo.Resolve(description);
            return target;
        }

        public async Task<TTarget> QueryAsync<TTarget>(string resource)
        where TTarget : class, ITransaction
        {
            var repo = _container.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveAsync(resource);
            return target as TTarget;
        }

        public TTarget Query<TTarget>(string resource)
        where TTarget : class, ITransaction
        {
            var repo = _container.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.Resolve(resource);
            return target as TTarget;
        }

        public void Dispose()
        {
            _container = null;
        }
    }
}