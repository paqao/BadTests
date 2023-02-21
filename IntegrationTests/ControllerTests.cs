using FluentAssertions;
using IntegrationTests.TestEngine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using WebApp.Controllers;

namespace IntegrationTests
{
    [Trait("Type", "Integration")]
    [Trait("Kind", "Controller")]
    public class ControllerTests : IAsyncLifetime
    {
        private readonly IntegrationTestEngine _testEngine;

        public ControllerTests()
        {
            _testEngine = new IntegrationTestEngine();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            await _testEngine.InitializeAsync();
        }

        [Fact]
        public async Task SimplyTest()
        {
            // Arrange
            var testItem = await _testEngine.BusinessProcessRepository.Create(new Logic.Models.BusinessProcess
            {
                Status = Logic.Const.BusinessProcessConstants.Status.New,
                Division = "IT"
            });

            // Act
            var data = await _testEngine.Processes.ApproveBusinessProcessAsync(testItem.Id);

            // Assert
            data.Division.Should().Be("IT");
            data.Status.Should().Be(Logic.Const.BusinessProcessConstants.Status.Approved);
        }

    }
}