using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Dto;
using Cinary.Finance.Qif.Transaction;
using Cinary.Framework.Modifier;
using System;
using System.Linq;

namespace Cinary.Finance.Qif.Modifier
{
    public abstract class TransactionModifier<TTransaction> : AbstractModifier<TransactionDto<TTransaction>, ITransactionDetail> where TTransaction : ITransaction
    {
        public override abstract ITransactionDetail Modify(params TransactionDto<TTransaction>[] parameters);

        protected void FillCategory(ICategory cat, string data)
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
