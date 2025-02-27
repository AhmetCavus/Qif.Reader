﻿using System;
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
        private string[] _transactionTypesToIgnore = { "cat", "class" };

        public QifStream(StreamReader stream)
        {
            _in = stream;
        }

        public IList<ITransactionEntry> ReadTransactions<T>() where T : ITransactionEntry
        {
            // Go to start
            _in.BaseStream.Position = 0;

            // Read header line
            string header = string.Empty;
            do
            {
                header = _in.ReadLine();
            } while (!IsHeaderValid(header));

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
            var mapper = ResolveMapper<T>();

            // Go to start
            _in.BaseStream.Position = 0;

            var transactions = new List<ITransactionEntry>();
            var transactionType = string.Empty;

            while (!_in.EndOfStream)
            {
                var line = string.Empty;
                var transactionString = string.Empty;
                while(!IsNextTransaction(line))
                {
                    line = await _in.ReadLineAsync();
                    if(line.ToLowerInvariant().StartsWith("!type:"))
                    {
                        transactionType = ResolveTransactionType(line);
                        break;
                    } else
                    {
                        transactionString += line + Environment.NewLine;
                    }
                }
                var normalizedTransactionType = transactionType.ToLowerInvariant();
                if (string.IsNullOrEmpty(transactionType)) continue;
                if (_transactionTypesToIgnore.Contains(normalizedTransactionType)) continue;
                CreateAndAddTransaction(transactions, mapper, transactionType, transactionString);
            }
            return transactions;
        }

        void CreateAndAddTransaction<TType>(IList<ITransactionEntry> transactions, TransactionMap<TType> mapper, string transactionType, string transactionString) where TType : ITransactionEntry
        {
            if (string.IsNullOrEmpty(transactionString) || transactionString.StartsWith("!")) return;
            var transactionToAdd = CreateTransaction(mapper, transactionType, transactionString);
            if (string.IsNullOrEmpty(transactionToAdd.PayeeAccount) && transactionToAdd.Date <= DateTime.MinValue) return;
            transactions.Add((TType)Convert.ChangeType(transactionToAdd, typeof(TType)));
        }

        TType CreateTransaction<TType>(TransactionMap<TType> mapper, string transactionType, string transactionString) where TType : ITransactionEntry
        {
            // Parse, map and add to list
            var transaction = mapper.Parse<TType>(transactionString);
            transaction.Type = transactionType;
            return transaction;
        }

        string ResolveTransactionType(string header)
        {
            // Get transaction type
            var transactionType = string.Empty;
            if (IsHeaderValid(header))
            {
                transactionType = header.Substring(6).ToUpper();
            }
            else
            {
                throw new FormatException("Invalid Header format;");
            }
            return transactionType;
        }

        TransactionMap<TType> ResolveMapper<TType>() where TType : ITransactionEntry
        {
            var (mapperClass, mapperName) = ResolveMapperClass<TType>();
            if (mapperClass == null)
                throw new NotImplementedException("The class '" + mapperName + "' was not found.");
            var mapper = (TransactionMap<TType>)Activator.CreateInstance(mapperClass.AsType());
            return mapper;
        }

        (TypeInfo typeInfo, string mapperName) ResolveMapperClass<TType>()
        {
            // Use convention to find the mapper
            var mapperName = typeof(TType).Name + "Map";
            var assembly = GetType().GetTypeInfo().Assembly;
            var toFind = $"{GetType().Namespace}.Data.TransactionMapping.{mapperName}";
            var mapperClass = (from item in assembly.DefinedTypes
                               where item.FullName == toFind
                               select item).FirstOrDefault();
            return (mapperClass, mapperName);
        }

        bool IsHeaderValid(string header) => header != null && header.ToLowerInvariant().StartsWith("!type:");

        bool IsNextTransaction(string line) => line.StartsWith("^");

        bool IsNewTransactionType(string line) => line.ToLowerInvariant().StartsWith("!type:");
    }
}