using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Service;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif
{
    public class ITransactionReader : Service.ITransactionReader
    {
        public async Task<IList<TType>> ReadFromResourceAsync<TType>(string resource) where TType : ITransaction
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);

            List<TType> transactions;
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = await qif.ReadTransactionsAsync<TType>();
            }
            return transactions;
        }

        public IList<TType> ReadFromResource<TType>(string resource) where TType : ITransaction
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resource);

            List<TType> transactions;
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = qif.ReadTransactions<TType>();
            }
            return transactions;
        }

        public async Task<IList<TType>> ReadAsync<TType>(string resource) where TType : ITransaction
        {
            Stream stream = File.OpenRead(resource);

            List<TType> transactions;
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = await qif.ReadTransactionsAsync<TType>();
            }
            return transactions;
        }

        public IList<TType> Read<TType>(string resource) where TType : ITransaction
        {
            Stream stream = File.OpenRead(resource);

            List<TType> transactions;
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1"), true))
            {
                QifStream qif = new QifStream(reader);
                transactions = qif.ReadTransactions<TType>();
            }
            return transactions;
        }
    }
}