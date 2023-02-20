using Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Logic.Services
{
    public class SomeOtherTests
    {
        [Fact]
        public void SequentialTest()
        {
            var clientMock = new Mock<IExternalClient>();
            clientMock.SetupSequence(x => x.CountItems())
                .ReturnsAsync(3)
                .ReturnsAsync(4)
                .ReturnsAsync(4);
        }
    }
}
