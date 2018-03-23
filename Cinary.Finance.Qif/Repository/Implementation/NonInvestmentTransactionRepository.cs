//
//  TransactionModelRepo.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

using System.Collections.Generic;
using Cinary.Finance.Qif.Data.Modifier.Implementation;
using Cinary.Finance.Qif.Dto;
using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Transaction;
using Cinary.Finance.Qif.TransactionTypes;

namespace Cinary.Finance.Qif.Repository
{
    public class NonInvestmentTransactionRepository : GenericTransactionRepository<NonInvestmentTransaction>
    {

        #region Attributes

        NonInvestmentModifier _modifier = new NonInvestmentModifier();

        #endregion


        #region Constructor

        public NonInvestmentTransactionRepository() { }

        public NonInvestmentTransactionRepository(ITransactionReader reader)
        {
            _reader = reader;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Protected / Private Methods

        protected override ITransactionDetail OnCreate(string id, IList<NonInvestmentTransaction> transactions)
        {
            ITransactionDetail result = default(ITransactionDetail);
            var data = new TransactionDto<NonInvestmentTransaction>() { Transactions = transactions };
            result = _modifier.Modify(data);
            _container[id] = result;
            return result;
        }

        #endregion
    }
}
