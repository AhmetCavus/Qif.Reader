//
//  DataService.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Repository;
using Cinary.Finance.Qif.Service.Interface;
using Cinary.Finance.Qif.Transaction;

namespace Cinary.Finance.Qif
{
    public class QifService : IQifService
    {
        private IQifReader _qifReader;
        private IQifRepositoryContainer _qifRepoLocator;

        public QifService(IQifReader qifReader, IQifRepositoryContainer qifRepositoryConainer)
        {
            _qifReader = qifReader;
            _qifRepoLocator = qifRepositoryConainer;
        }

        public async Task<IList<ITransactionEntry>> QueryAsync<TTarget>(string id, Stream resource)
        where TTarget : class, ITransactionEntry
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveAsync(id, resource);
            return target;
        }

        public IList<ITransactionEntry> Query<TTarget>(string id, Stream resource)
        where TTarget : class, ITransactionEntry
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.Resolve(id, resource);
            return target;
        }

        public async Task<IList<ITransactionEntry>> QueryFromFileAsync<TTarget>(string path)
        where TTarget : class, ITransactionEntry
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveFromFileAsync(path);
            return target;
        }

        public IList<ITransactionEntry> QueryFromFile<TTarget>(string path)
        where TTarget : class, ITransactionEntry
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = repo.ResolveFromFile(path);
            return target;
        }

       public async Task<IList<ITransactionEntry>> QueryFromResourceAsync<TTarget>(string resource)
       where TTarget : class, ITransactionEntry
        {
            var repo = _qifRepoLocator.Resolve<TTarget>();
            repo.SetTransactionReader(_qifReader);
            var target = await repo.ResolveFromResourceAsync(resource);
            return target;
        }

        public IList<ITransactionEntry> QueryFromResource<TTarget>(string resource)
        where TTarget : class, ITransactionEntry
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