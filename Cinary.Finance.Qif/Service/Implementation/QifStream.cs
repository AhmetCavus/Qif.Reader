using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cinary.Finance.Qif.Transaction;
using Cinary.Finance.Qif.TransactionMapping;

namespace Cinary.Finance.Qif
{
    internal class QifStream
    {
        private StreamReader _in;

        public QifStream(StreamReader stream)
        {
            _in = stream;
        }

        public IList<ITransactionEntry> ReadTransactions<T>() where T : ITransactionEntry
        {
            // Go to start
            _in.BaseStream.Position = 0;

            // Read header line
            var header = _in.ReadLine();

            // Get transaction type
            var transactionType = string.Empty;
            if (header.StartsWith("!Type:"))
            {
                transactionType = header.Substring(6).ToUpper();
            }
            else
            {
                throw new FormatException("Invalid Header format;");
            }

            // Go to start
            _in.BaseStream.Position = 1;

            // dummy proof
            var transactions = new List<ITransactionEntry>();

            // Use convention to find the mapper
            var mapperName = typeof(T).Name + "Map";
            var assembly = GetType().GetTypeInfo().Assembly;
            var toFind = $"{GetType().Namespace}.Data.TransactionMapping.{mapperName}";
            var mapperClass = (from item in assembly.DefinedTypes
                               where item.FullName == toFind
                               select item).FirstOrDefault();

            if (mapperClass == null)
                throw new NotImplementedException("The class '" + mapperName + "' was not found.");

            var mapper = (TransactionMap<T>)Activator.CreateInstance(mapperClass.AsType());

            // Get the next transaction
            string line = string.Empty;
            while (_in.BaseStream.Position < _in.BaseStream.Length - 1)
            {
                var transaction = (T)Activator.CreateInstance(typeof(T));
                string transactionstring = string.Empty;
                while (!(line = _in.ReadLine()).StartsWith("^"))
                {
                    transactionstring += line + Environment.NewLine;
                }
                // Parse, map and add to list
                transaction = mapper.Parse<T>(transactionstring);
                transaction.Type = transactionType;
                transactions.Add((T)Convert.ChangeType(transaction, typeof(T)));
            }
            return transactions;
        }

        public async Task<IList<ITransactionEntry>> ReadTransactionsAsync<T>() where T : ITransactionEntry
        {
            // Go to start
            _in.BaseStream.Position = 0;

            // Read header line
            var header = await _in.ReadLineAsync();

            // Get transaction type
            var transactionType = string.Empty;
            if (header.StartsWith("!Type:"))
            {
                transactionType = header.Substring(6).ToUpper();
            }
            else
            {
                throw new FormatException("Invalid Header format;");
            }

            // Go to start
            _in.BaseStream.Position = 1;


            // dummy proof
            var transactions = new List<ITransactionEntry>();

            // Use convention to find the mapper
            var mapperName = typeof(T).Name + "Map";
            var assembly = GetType().GetTypeInfo().Assembly;
            var toFind = $"{GetType().Namespace}.Data.TransactionMapping.{mapperName}";
            var mapperClass = (from item in assembly.DefinedTypes
                               where item.FullName == toFind
                               select item).FirstOrDefault();

            if (mapperClass == null)
                throw new NotImplementedException("The class '" + mapperName + "' was not found.");

            var mapper = (TransactionMap<T>)Activator.CreateInstance(mapperClass.AsType());

            // Get the next transaction
            string line = string.Empty;
            while(_in.BaseStream.Position < _in.BaseStream.Length - 1)
            {
                var transaction = (T)Activator.CreateInstance(typeof(T));
                string transactionstring = string.Empty;
                while (!(line = await _in.ReadLineAsync()).StartsWith("^"))
                {
                    transactionstring += line + Environment.NewLine;
                }
                // Parse, map and add to list
                transaction = mapper.Parse<T>(transactionstring);
                transaction.Type = transactionType;
                transactions.Add((T)Convert.ChangeType(transaction,typeof(T)));
            }
            return transactions;
        }
    }
}