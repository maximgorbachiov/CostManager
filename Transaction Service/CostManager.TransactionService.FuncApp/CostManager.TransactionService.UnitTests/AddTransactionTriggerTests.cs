using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using CostManager.TransactionService.FuncApp;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CostManager.TransactionService.UnitTests
{
    public class AddTransactionTriggerTests
    {
        private readonly AddTransactionTrigger _addTransactionTrigger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger _logger;

        public AddTransactionTriggerTests()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _addTransactionTrigger = new AddTransactionTrigger(_transactionRepository);
            _logger = Substitute.For<ILogger<AddTransactionTrigger>>();
        }

        [Fact]
        public async Task Run_ShoultReturnBadRequestObjectResult_WhenCategoryIdIsEmpty()
        {
            // Arrange
            var requestModel = new AddTransactionModel { CategoryId = string.Empty };

            // Act
            var response = _addTransactionTrigger.Run(requestModel, _logger);

            // Assert
            //string infoMessage = $"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.";
            //string errorMessage = $"{nameof(AddTransactionTrigger)}: {nameof(requestModel.CategoryId)} should not be empty";
            //_logger.Received(1).LogInformation(infoMessage);
            //_logger.Received(1).LogError(errorMessage);

            var result = response.Result as BadRequestObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be($"{nameof(AddTransactionTrigger)}: {nameof(requestModel.CategoryId)} should not be empty");
        }

        [Fact]
        public async Task Run_ShoultReturnOkObjectResult_WhenCategoryIdExists()
        {
            // Arrange
            var requestModel = new AddTransactionModel { CategoryId = Guid.NewGuid().ToString() };
            string newAddedTransactionId = Guid.NewGuid().ToString();

            _transactionRepository.AddTransactionAsync(Arg.Any<AddTransactionModel>()).Returns(newAddedTransactionId);

            // Act
            var response = _addTransactionTrigger.Run(requestModel, _logger);

            // Assert
            //_logger.Received(1).LogInformation($"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.");

            var result = response.Result as OkObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(newAddedTransactionId);
        }
    }
}