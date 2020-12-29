using AutoFixture;
using Cinary.Finance.Qif.Repository;
using Cinary.Finance.Qif.Service;
using Cinary.Finance.Qif.Service.Interface;
using Cinary.Finance.Qif.Transaction;
using Cinary.Finance.Qif.TransactionTypes;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Test.Service
{
    public class QifServiceQueryAsyncTests
    {
        [Test]
        public async Task QifService_QueryAsync_VerifyQifRepositoryContainerIsCalled()
        {
            // Arrange
            var (id, stream, qifRepoMock, qifReaderMock, qifRepositoryContainerMock, transactions, sut) = Arrange(countOfTransactions: 10);

            // Act
            var result = await sut.QueryAsync<NonInvestmentTransaction>(id, stream);

            // Assert
            qifRepositoryContainerMock.Verify(x => x.Resolve<NonInvestmentTransaction>(), Times.Once);
        }

        [Test]
        public async Task QifService_QueryAsync_VerifyQifRepositoryCallsResolve()
        {
            // Arrange
            var (id, stream, qifRepoMock, qifReaderMock, qifRepositoryContainerMock, transactions, sut) = Arrange(countOfTransactions: 10);

            // Act
            var result = await sut.QueryAsync<NonInvestmentTransaction>(id, stream);

            // Assert
            qifRepoMock.Verify(x => x.ResolveAsync(It.Is<string>(x => x == id), It.Is<Stream>(x => x == stream)), Times.Once);
        }

        [Test]
        public async Task QifService_QueryAsync_VerifIfResultIsEqualToExpectedList()
        {
            // Arrange
            var (id, stream, _, _, _, transactions, sut) = Arrange(10);

            // Act
            var result = await sut.QueryAsync<NonInvestmentTransaction>(id, stream);

            // Assert
            CollectionAssert.AreEqual(transactions, result);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public async Task QifService_QueryAsync_VerifyIfListMatchesGivenCount(int count)
        {
            // Arrange
            var (id, stream, _, _, _, _, sut) = Arrange(count);

            // Act
            var result = await sut.QueryAsync<NonInvestmentTransaction>(id, stream);

            // Assert
            Assert.That(result.Count, Is.EqualTo(count));
        }

        (string id, Stream stream, Mock<NonInvestmentTransactionRepository> qifRepoMock, Mock<IQifReader> qifReaderMock, Mock<IQifRepositoryContainer> qifRepoContainerMock, IList<ITransactionEntry> transactions, ITransactionService sut) Arrange(int countOfTransactions)
        {
            var fixture = new Fixture();
            var id = fixture.Create<string>();

            fixture.
                Register<ITransactionEntry>(() => fixture.Create<NonInvestmentTransaction>());

            IList<ITransactionEntry> transactions = fixture.CreateMany<ITransactionEntry>(countOfTransactions).ToList();

            var stream = new Mock<Stream>().Object;

            var qifRepoMock = new Mock<NonInvestmentTransactionRepository>();
            qifRepoMock
                .Setup(qifRepository => qifRepository.ResolveAsync(id, stream))
                .ReturnsAsync(transactions);

            var qifRepositoryContainerMock = new Mock<IQifRepositoryContainer>();
            qifRepositoryContainerMock
                .Setup(qifRepositoryContainer => qifRepositoryContainer.Resolve<NonInvestmentTransaction>())
                .Returns(qifRepoMock.Object);

            var qifReaderMock = new Mock<IQifReader>();

            ITransactionService sut = new QifService(qifReaderMock.Object, qifRepositoryContainerMock.Object);

            return (id, stream, qifRepoMock, qifReaderMock, qifRepositoryContainerMock, transactions, sut);
        }
    }
}
