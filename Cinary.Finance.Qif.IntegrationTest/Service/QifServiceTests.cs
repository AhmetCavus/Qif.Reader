using Cinary.Finance.Qif.Repository;
using Cinary.Finance.Qif.Service.Interface;
using Cinary.Finance.Qif.TransactionTypes;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.IntegrationTest.Service
{
    public class QifServiceTests
    {
        string _qifFilePath = "/export.qif";
        IQifService _qifService;

        [SetUp]
        public void Init()
        {
            // Arrange
            _qifFilePath = $"{Environment.CurrentDirectory}\\export.qif";
            _qifService = new QifService(new QifReader(), new QifRepositoryContainer());
        }

        [TearDown]
        public void Dispose()
        {
            _qifService.Dispose();
        }


        [Test]
        [Explicit("Depends on a qif file. This file had to be provided before executing this test")]
        public async Task QifService_QueryFromFile_CheckIfResultIsNotNull()
        {
            // Arrange


            // Act
            var transactions = await _qifService.QueryFromFileAsync<NonInvestmentTransaction>(_qifFilePath);

            // Assert
            Assert.NotNull(transactions);
        }

        [Test]
        [Explicit("Depends on a qif file. This file had to be provided before executing this test")]
        public void QifService_QueryFromFileTwice_DoesNotThrowException()
        {
            // Arrange

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                var transactions1 = await _qifService.QueryFromFileAsync<NonInvestmentTransaction>(_qifFilePath);
                var transactions2 = await _qifService.QueryFromFileAsync<NonInvestmentTransaction>(_qifFilePath);
            });
        }


    }
}
