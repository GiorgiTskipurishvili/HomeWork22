using Homework22.Controllers;
using Moq;
using System;
using Xunit;

namespace surveyRespondent_Tests
{
	public class PollingControllerTests
	{
		private readonly Mock<PollingService> _mockPollingService;
		private readonly PollingController _controller;

		public PollingControllerTests()
		{
			_mockPollingService = new Mock<IPollingService>();
			_controller = new PollingController(_mockPollingService.Object);
		}


		[Fact]
		public void AddSurveyRespondent_ValidPerson_ReturnsOkResult()
		{
			// Arrange
			var person = new Person
			{
				// Set valid person properties
			};
			_mockPollingService.Setup(service => service.AddSurveyRespondent(person)).Returns(person);

			// Act
			var result = _controller.AddSurveyRespondent(person);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
