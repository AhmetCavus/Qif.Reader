using Cinary.Finance.Qif.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class AbstractQifRepositoryContainer : ITransactionRepositoryContainer
    {
        protected static Dictionary<string, ITransactionRepository> _container = new Dictionary<string, ITransactionRepository>();

        public ITransactionRepository Resolve(Type transactionType)
        {
            string resourceNameRepo = transactionType.Name.Replace("Model", "").Replace("Resource", "") + "Repository";

            ITransactionRepository result = default(ITransactionRepository);
            if (_container.ContainsKey(resourceNameRepo))
            {
                result = _container[resourceNameRepo];
            }
            else
            {
                // Use naming convention to find the repository
                var assembly = transactionType.GetTypeInfo().Assembly;
                var repoClass = (from item in assembly.DefinedTypes
                                 where item.FullName.Contains(resourceNameRepo)
                                 select item).FirstOrDefault();

                if (repoClass == null)
                    throw new NotImplementedException("The class '" + resourceNameRepo + "' was not found.");

                try
                {
                    result = (ITransactionRepository) Activator.CreateInstance(repoClass.AsType());
                }
                catch (Exception err)
                {
                    System.Console.WriteLine(err);
                    throw err;
                }
                _container.Add(transactionType.Name, result);
            }
            return result;
        }

        public ITransactionRepository<TTarget> Resolve<TTarget>() where TTarget : ITransaction => Resolve(typeof(TTarget)) as ITransactionRepository<TTarget>;

        public void Dispose()
        {
            _container.Clear();
            _container = null;
        }

    }
}