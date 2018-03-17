using Cinary.Finance.Qif.Data.Dto;
using Cinary.Finance.Qif.Data.Model;
using Cinary.Framework.Data.Modifier;
using System;
using System.Linq;

namespace Cinary.Finance.Qif.Data.Modifier
{
    public class TransactionModelModifier : AbstractModifier<TransactionDto, QifModel>
    {

        //public override TransactionModel Modify(params TransactionDto[] parameters)
        //{
        //    if (parameters == null || parameters.Length <= 0) throw new ArgumentNullException("No parameters given");
        //    var dto = parameters[0];
        //    var result = new TransactionModel();
        //    var categories = dto.Transactions.OrderByDescending(o => o.Date).GroupBy(o => o.Category).ToList();
        //    foreach (var dtoCat in categories)
        //    {
        //        var cat = new Category();
        //        FillCategory(cat, dtoCat.Key);
        //        result.Categories.Add(cat);
        //        //var accounts = dtoCat.GroupBy(o => o.PayeeAccount).OrderBy(o => o.Key);
        //        foreach (var dtoTransaction in dtoCat)
        //        {
        //            var transaction = new Transaction
        //            {
        //                Date = dtoTransaction.Date,
        //                Amount = dtoTransaction.Amount,
        //                SplitAmount = dtoTransaction.SplitAmount,
        //                SplitAmounts = dtoTransaction.SplitAmounts,
        //                IsCleared = dtoTransaction.IsCleared,
        //                Num = dtoTransaction.Num,
        //                PayeeLines = dtoTransaction.PayeeLines,
        //                Memo = dtoTransaction.Memo,
        //                SplitMemo = dtoTransaction.SplitMemo,
        //                AddressLines = dtoTransaction.AddressLines,
        //                Category = dtoTransaction.Category,
        //                SplitCategory = dtoTransaction.SplitCategory,
        //                Type = dtoTransaction.Type,
        //                Address = dtoTransaction.Address,
        //                Payee = dtoTransaction.Payee,
        //                PayeeAccount = dtoTransaction.PayeeAccount,
        //                PayeeName = dtoTransaction.PayeeName
        //            };
        //            cat.Transactions.Add(transaction);
        //        }
        //    }
        //    result.Categories = result.Categories.OrderBy(c => c.Id).ToList();
        //    return result;
        //}

        public override QifModel Modify(params TransactionDto[] parameters)
        {
            if (parameters == null || parameters.Length <= 0) throw new ArgumentNullException("No parameters given");
            var dto = parameters[0];
            var result = new QifModel();
            var categories = dto.Transactions.OrderByDescending(o => o.Date).GroupBy(o => o.Category).ToList();
            foreach (var dtoCat in categories)
            {
                var cat = new Category();
                FillCategory(cat, dtoCat.Key);
                result.Categories.Add(cat);
                var accounts = dtoCat.GroupBy(o => o.PayeeAccount).OrderBy(o => o.Key);
                foreach (var dtoAccount in accounts)
                {
                    var transactionGroup = new TransactionGroup
                    {
                        Title = dtoAccount.Key
                    };
                    if (!string.IsNullOrEmpty(dtoAccount.Key)) transactionGroup.ShortName = dtoAccount.Key[0];
                    cat.TransactionGroups.Add(transactionGroup);
                    //var account = new Account { Name = dtoAccount.Key };
                    //cat.Accounts.Add(account);
                    foreach (var dtoTransaction in dtoAccount)
                    {
                        var transaction = new Transaction
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

        void FillCategory(Category cat, string data)
        {
            try
            {
                var res = string.Join("", data.Where(char.IsDigit));
                cat.Id = int.Parse(res);
            }
            catch (Exception)
            {
                cat.Id = -1;
            }
            try
            {
                var res = string.Join("", data.Where(c => !char.IsDigit(c)));
                var split = res.Split(':');

                var type = split[0].Trim();
                var title = split[1].Trim();

                cat.Type = type;
                cat.Title = title;
            }
            catch (Exception)
            {
                cat.Type = "Ivalid";
                cat.Title = "Unknown";
            }
        }
    }
}
