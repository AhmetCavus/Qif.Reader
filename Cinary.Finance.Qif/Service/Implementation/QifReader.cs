using Cinary.Finance.Qif.Service;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif
{
    public class QifReader : ITransactionReader
    {
        public async Task<IList<TTarget>> ReadFromResourceAsync<TTarget>(string resource) where TTarget : ITransaction
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);
            return await ReadAsync<TTarget>(stream);
        }

        public IList<TTarget> ReadFromResource<TTarget>(string resource) where TTarget : ITransaction
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);
            return Read<TTarget>(stream);
        }

        public async Task<IList<TTarget>> ReadFromFileAsync<TTarget>(string resource) where TTarget : ITransaction
        {
            Stream stream = File.OpenRead(resource);
            return await ReadAsync<TTarget>(stream);
        }

        public IList<TTarget> ReadFromFile<TTarget>(string resource) where TTarget : ITransaction
        {
            Stream stream = File.OpenRead(resource);
            return Read<TTarget>(stream);
        }

        public async Task<IList<TTarget>> ReadAsync<TTarget>(Stream resource) where TTarget : ITransaction
        {
            List<TTarget> transactions;
            using (StreamReader reader = new StreamReader(resource, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = await qif.ReadTransactionsAsync<TTarget>();
            }
            return transactions;
        }

        public IList<TTarget> Read<TTarget>(Stream resource) where TTarget : ITransaction
        {
            List<TTarget> transactions;
            using (StreamReader reader = new StreamReader(resource, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = qif.ReadTransactions<TTarget>();
            }
            return transactions;
        }
    }
}