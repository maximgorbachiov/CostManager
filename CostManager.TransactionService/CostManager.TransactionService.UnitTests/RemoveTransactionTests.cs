using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CostManager.TransactionService.UnitTests
{
    public class RemoveTransactionTests
    {
        private readonly TransactionServiceController _transactionServiceController;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionServiceController> _logger;

        public RemoveTransactionTests()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _logger = Substitute.For<ILogger<TransactionServiceController>>();
            _transactionServiceController = new TransactionServiceController(_logger, _transactionRepository);
        }

        [Fact]
        public async Task Run_ShoultReturnBadRequestObjectResult_WhenCategoryIdIsEmpty()
        {
            // Arrange
            string transactionId = string.Empty;

            // Act
            var response = await _transactionServiceController.DeleteTransaction(transactionId);

            // Assert
            var result = response as BadRequestObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be($"DeleteTransaction: {nameof(transactionId)} should not be empty");
        }

        [Fact]
        public async Task Run_ShoultReturnOkObjectResult_WhenCategoryIdExists()
        {
            // Arrange
            string transactionId = Guid.NewGuid().ToString();
            _transactionRepository.RemoveTransactionAsync(Arg.Any<string>()).Returns(true);

            // Act
            var response = await _transactionServiceController.DeleteTransaction(transactionId);

            // Assert
            var result = response as OkObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(true);
        }
    }
}
