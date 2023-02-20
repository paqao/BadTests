using Logic.Services;
using Moq;
using Moq.Protected;
using Moq.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Logic.Services
{
    public class ExternalClientTests
    {
        [Fact]
        public async Task TestIfProperServiceIsRequested()
        {
            // arrange
            var httpResponse = new HttpResponseMessage
            {
                Content = new StringContent("3")
            };
            var mock = new Mock<HttpMessageHandler>();

            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);
            var httpClient = new HttpClient(mock.Object);
            var client = new ExternalClient(httpClient);

            var response = await client.CountItems();
            // act


            // assert
        }
    }
}
