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
            ITransactionDetail result = service.QueryFromFile<NonInvestmentTransaction>(_path + "/export.qif");

            foreach (var transaction in result.Categories)
            {
                Output.WriteLine($"{transaction.Id}, {transaction.Title}, {transaction.Type}");
                foreach (var category in transaction.TransactionGroups)
                {
                    Output.WriteLine($"\t - {category.Title}, {category.ShortName}, {category.Detail}");
                }
            }
        }
    }
}
