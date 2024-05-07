using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using CostManager.TransactionService.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CostManager.TransactionService.UnitTests
{
    public class AddTransactionTests
    {
        private readonly TransactionServiceController _transactionServiceController;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionServiceController> _logger;

        public AddTransactionTests()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _logger = Substitute.For<ILogger<TransactionServiceController>>();
            _transactionServiceController = new TransactionServiceController(_logger, _transactionRepository);
        }

        [Fact]
        public async Task Run_ShoultReturnBadRequestObjectResult_WhenCategoryIdIsEmpty()
        {
            // Arrange
            var requestModel = new AddTransactionModel { CategoryId = string.Empty };

            // Act
            var response = _transactionServiceController.AddTransaction(requestModel);

            // Assert
            var result = response.Result as BadRequestObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be($"AddTransaction: {nameof(requestModel.CategoryId)} should not be empty");
        }

        [Fact]
        public async Task Run_ShoultReturnOkObjectResult_WhenCategoryIdExists()
        {
            // Arrange
            var requestModel = new AddTransactionModel { CategoryId = Guid.NewGuid().ToString() };
            string newAddedTransactionId = Guid.NewGuid().ToString();

            _transactionRepository.AddTransactionAsync(Arg.Any<AddTransactionModel>()).Returns(newAddedTransactionId);

            // Act
            var response = _transactionServiceController.AddTransaction(requestModel);

            // Assert
            //_logger.Received(1).LogInformation($"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.");

            var result = response.Result as OkObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(newAddedTransactionId);
        }
    }
}