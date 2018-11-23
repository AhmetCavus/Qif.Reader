using Cinary.Finance.Qif.Data;
using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Transaction;
using Cinary.Finance.Qif.TransactionTypes;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Cinary.Finance.Qif.Test
{
    public class MainTest
    {
        public ITestOutputHelper Output { get; }
        string _path;

        public MainTest(ITestOutputHelper output)
        {
            _path = Environment.CurrentDirectory;
            Output = output;
        }

        [Fact]
        public void StartTest()
        {
            ITransactionService service = new QifService();
            var transactions = service.QueryFromFile<NonInvestmentTransaction>(_path + "/export.qif");

            foreach (var transaction in transactions)
            {
                Output.WriteLine($"{transaction.Id}, {transaction.Category}, {transaction.Type}");
            }
        }
    }
}
