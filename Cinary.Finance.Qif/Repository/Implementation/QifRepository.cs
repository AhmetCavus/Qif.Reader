//
//  TransactionModelRepo.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System.Threading.Tasks;
using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Data.Description;
using Cinary.Finance.Qif.Data.Dto;
using Cinary.Finance.Qif.Data.Model;
using Cinary.Finance.Qif.Data.Modifier;
using Cinary.Finance.Qif.Data.TransactionTypes;

namespace Cinary.Finance.Qif.Repository
{
    public class QifRepository : AbstractQifRepository<QifModel>
    {
        TransactionModelModifier _modifier = new TransactionModelModifier();

        public QifRepository() { }

        public QifRepository(Service.ITransactionReader qif)
        {
            _qif = qif;
        }

        public override async Task<QifModel> ResolveAsync(IDataDescription description = null)
        {
            QifModel result = default(QifModel);
            if(_container.ContainsKey(description.Resource))
            {
                result = _container[description.Resource];
            } else
            {
                var list = await _qif.ReadAsync<NonInvestmentTransaction>(description?.Resource);
                var data = new TransactionDto() { Transactions = list };
                result = _modifier.Modify(data);
                _container[description.Resource] = result;
            }

            return result;
        }

        public override QifModel Resolve(IDataDescription description = null)
        {
            var list = _qif.Read<NonInvestmentTransaction>(description?.Resource);
            var data = new TransactionDto() { Transactions = list };
            var model = _modifier.Modify(data);
            return model;
        }

        public override async Task<ITransaction> ResolveAsync(string resource)
        {
            return await ResolveAsync(new QifDescription(resource));
        }

        public override ITransaction Resolve(string resource)
        {
            return Resolve(new QifDescription(resource));
        }
    }
}
