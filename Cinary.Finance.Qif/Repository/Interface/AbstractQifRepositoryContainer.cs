using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cinary.Finance.Qif.Repository
{
    public abstract class AbstractQifRepositoryContainer : ITransactionRepositoryContainer
    {
        protected static Dictionary<string, AbstractTransactionRepository> _container = new Dictionary<string, AbstractTransactionRepository>();

        public AbstractTransactionRepository Resolve(Type transactionType)
        {
            string resourceNameRepo = transactionType.Name.Replace("Model", "").Replace("Resource", "") + "Repository";

            AbstractTransactionRepository result = default(AbstractTransactionRepository);
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
                    result = (AbstractTransactionRepository) Activator.CreateInstance(repoClass.AsType());
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

        public AbstractTransactionRepository Resolve<TTarget>() where TTarget : ITransactionEntry => Resolve(typeof(TTarget));

        public void Dispose()
        {
            _container.Clear();
            _container = null;
        }

    }
}