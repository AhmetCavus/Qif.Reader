//
//  DataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System.IO;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Repository;
using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif.Data
{
    public class QifService : ITransactionService
    {
        protected QifReader _qifReader = new QifReader();
        protected QifRepositoryContainer _qifRepoLocator = new QifRepositoryContainer();

        public async Task<ITransactionDetail> QueryAsync<TTarget>(string id, Stream resource)
        where TTarget : class, ITransaction
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveAsync(id, resource);
            return target;
        }

        public ITransactionDetail Query<TTarget>(string id, Stream resource)
        where TTarget : class, ITransaction
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.Resolve(id, resource);
            return target;
        }

        public async Task<ITransactionDetail> QueryFromFileAsync<TTarget>(string path)
        where TTarget : class, ITransaction
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveFromFileAsync(path);
            return target;
        }

        public ITransactionDetail QueryFromFile<TTarget>(string path)
        where TTarget : class, ITransaction
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.ResolveFromFile(path);
            return target;
        }

       public async Task<ITransactionDetail> QueryFromResourceAsync<TTarget>(string resource)
       where TTarget : class, ITransaction
       {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveFromResourceAsync(resource);
            return target;
        }

        public ITransactionDetail QueryFromResource<TTarget>(string resource)
        where TTarget : class, ITransaction
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.ResolveFromResource(resource);
            return target;
        }

        public void Dispose()
        {
            _qifRepoLocator = null;
            _qifReader = null;
        }

    }
}