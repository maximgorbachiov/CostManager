using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.FuncApp;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CostManager.TransactionService.UnitTests
{
    public class RemoveTransactionTriggerTests
    {
        private readonly RemoveTransactionTrigger _removeTransactionTrigger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger _logger;

        public RemoveTransactionTriggerTests()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _removeTransactionTrigger = new RemoveTransactionTrigger(_transactionRepository);
            _logger = Substitute.For<ILogger<RemoveTransactionTrigger>>();
        }

        [Fact]
        public async Task Run_ShoultReturnBadRequestObjectResult_WhenCategoryIdIsEmpty()
        {
            // Arrange
            string transactionId = string.Empty;

            var req = new DefaultHttpContext();
            req.Request.QueryString = new QueryString($"?transactionId={transactionId}");

            // Act
            var response = await _removeTransactionTrigger.Run(req.Request, _logger);

            // Assert
            //string infoMessage = $"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.";
            //string errorMessage = $"{nameof(AddTransactionTrigger)}: {nameof(requestModel.CategoryId)} should not be empty";
            //_logger.Received(1).LogInformation(infoMessage);
            //_logger.Received(1).LogError(errorMessage);

            var result = response as BadRequestObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be($"{nameof(RemoveTransactionTrigger)}: {nameof(transactionId)} should not be empty");
        }

        [Fact]
        public async Task Run_ShoultReturnOkObjectResult_WhenCategoryIdExists()
        {
            // Arrange
            string transactionId = Guid.NewGuid().ToString();

            var req = new DefaultHttpContext();
            req.Request.QueryString = new QueryString($"?transactionId={transactionId}");

            _transactionRepository.RemoveTransactionAsync(Arg.Any<string>()).Returns(true);

            // Act
            var response = await _removeTransactionTrigger.Run(req.Request, _logger);

            // Assert
            //string infoMessage = $"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.";
            //string errorMessage = $"{nameof(AddTransactionTrigger)}: {nameof(requestModel.CategoryId)} should not be empty";
            //_logger.Received(1).LogInformation(infoMessage);
            //_logger.Received(1).LogError(errorMessage);

            var result = response as OkObjectResult;
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(true);
        }
    }
}
