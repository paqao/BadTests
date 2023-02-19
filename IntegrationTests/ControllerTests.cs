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
    public class ControllerTests : IClassFixture<IntegrationTestEngine>
    {
        private readonly IntegrationTestEngine _testEngine;

        public ControllerTests(IntegrationTestEngine testEngine)
        {
            _testEngine = testEngine;
        }

        [Fact]
        public async Task HelloWorldTest()
        {
            // Arrange
            var data = _testEngine.Processes.ApproveBusinessProcessAsync(1);

            //var url = _testEngine.

        }
    }
}