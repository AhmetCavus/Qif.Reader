using Cinary.Finance.Qif.Dto;
using Cinary.Finance.Qif.Modifier;
using Cinary.Finance.Qif.Transaction;
using Cinary.Finance.Qif.TransactionTypes;
using System;
using System.Linq;

namespace Cinary.Finance.Qif.Data.Modifier.Implementation
{
    public class NonInvestmentModifier : TransactionModifier<NonInvestmentTransaction>
    {
        public override ITransactionGroup Modify(params TransactionDto<NonInvestmentTransaction>[] parameters)
        {
            if (parameters == null || parameters.Length <= 0) throw new ArgumentNullException("No parameters given");
            var dto = parameters[0];
            var result = new TransactionModel();
            var categories = dto.Transactions.OrderByDescending(o => o.Date).GroupBy(o => o.Category).ToList();
            foreach (var dtoCat in categories)
            {
                var cat = new Category();
                FillCategory(cat, dtoCat.Key);
                result.Categories.Add(cat);
                var accounts = dtoCat.GroupBy(o => o.PayeeAccount).OrderBy(o => o.Key);
                foreach (var dtoAccount in accounts)
                {
                    var transactionGroup = new TransactionEntryGroup
                    {
                        Title = dtoAccount.Key
                    };
                    if (!string.IsNullOrEmpty(dtoAccount.Key)) transactionGroup.ShortName = dtoAccount.Key[0];
                    cat.TransactionGroups.Add(transactionGroup);
                    //var account = new Account { Name = dtoAccount.Key };
                    //cat.Accounts.Add(account);
                    foreach (var dtoTransaction in dtoAccount)
                    {
                        var transaction = new TransactionEntry
                        {
                            Date = dtoTransaction.Date,
                            Amount = dtoTransaction.Amount,
                            SplitAmount = dtoTransaction.SplitAmount,
                            SplitAmounts = dtoTransaction.SplitAmounts,
                            IsCleared = dtoTransaction.IsCleared,
                            Num = dtoTransaction.Num,
                            PayeeLines = dtoTransaction.PayeeLines,
                            Memo = dtoTransaction.Memo,
                            SplitMemo = dtoTransaction.SplitMemo,
                            AddressLines = dtoTransaction.AddressLines,
                            Category = dtoTransaction.Category,
                            SplitCategory = dtoTransaction.SplitCategory,
                            Type = dtoTransaction.Type,
                            Address = dtoTransaction.Address,
                            Payee = dtoTransaction.Payee,
                            PayeeAccount = dtoTransaction.PayeeAccount,
                            PayeeName = dtoTransaction.PayeeName
                        };
                        transactionGroup.Add(transaction);
                    }
                }
            }
            result.Categories = result.Categories.OrderBy(c => c.Id).ToList();
            return result;
        }
    }
}
