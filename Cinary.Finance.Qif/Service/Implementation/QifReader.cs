using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Service.Interface;
using Cinary.Finance.Qif.Transaction;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif
{
    public class QifReader : IQifReader
    {
        public async Task<IList<ITransactionEntry>> ReadFromResourceAsync<TTarget>(string resource) where TTarget : ITransactionEntry
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);
            return await ReadAsync<TTarget>(stream);
        }

        public IList<ITransactionEntry> ReadFromResource<TTarget>(string resource) where TTarget : ITransactionEntry
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);
            return Read<TTarget>(stream);
        }

        public async Task<IList<ITransactionEntry>> ReadFromFileAsync<TTarget>(string resource) where TTarget : ITransactionEntry
        {
            Stream stream = File.OpenRead(resource);
            return await ReadAsync<TTarget>(stream);
        }

        public IList<ITransactionEntry> ReadFromFile<TTarget>(string resource) where TTarget : ITransactionEntry
        {
            Stream stream = File.OpenRead(resource);
            return Read<TTarget>(stream);
        }

        public async Task<IList<ITransactionEntry>> ReadAsync<TTarget>(Stream resource) where TTarget : ITransactionEntry
        {
            IList<ITransactionEntry> transactions;
            using (StreamReader reader = new StreamReader(resource, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = await qif.ReadTransactionsAsync<TTarget>();
            }
            return transactions;
        }

        public IList<ITransactionEntry> Read<TTarget>(Stream resource) where TTarget : ITransactionEntry
        {
            IList<ITransactionEntry> transactions;
            using (StreamReader reader = new StreamReader(resource, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = qif.ReadTransactions<TTarget>();
            }
            return transactions;
        }
    }
}