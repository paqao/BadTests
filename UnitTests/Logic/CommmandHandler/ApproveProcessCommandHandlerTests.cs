using AutoMapper;
using FluentAssertions;
using Logic.CommandHandlers;
using Logic.Commands;
using Logic.Const;
using Logic.Exceptions;
using Logic.Models;
using Logic.Profiles;
using Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Logic.CommmandHandler
{
    public class ApproveProcessCommandHandlerTests
    {
        private readonly ApproveProcessCommandHandler _commandHandler;
        private readonly Mock<IRepository<BusinessProcess>> _repository;

        public ApproveProcessCommandHandlerTests()
        {
            _repository = new Mock<IRepository<BusinessProcess>>();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BusinessProcessProfile>();
            });

            _commandHandler = new ApproveProcessCommandHandler(_repository.Object, mapper.CreateMapper());
        }

        [Fact]
        public async Task When_BusinessProcessDontExist_Then_ThrowAnException()
        {
            // Arrange 
            _repository
                .Setup(s => s.GetById(1))
                .ReturnsAsync((BusinessProcess)null)
                .Verifiable();

            // Act
            var assertedException = await Assert.ThrowsAsync<NotFoundException>(() =>
                 _commandHandler.ExecuteAsync(new ApproveProcessCommand
                {
                    ProcessId = 1
                }));

            // Assert
            _repository.VerifyAll();
            _repository.VerifyNoOtherCalls();
            assertedException.Id.Should().Be(1);
        }

        [Fact]
        public async Task When_BusinessProcessExistAndStatusIsInvalid_Then_ThrowAnException()
        {
            // Arrange 
            _repository
                .Setup(s => s.GetById(1))
                .ReturnsAsync(new BusinessProcess
                {
                    Id = 1,
                    Status = BusinessProcessConstants.Status.Approved
                })
                .Verifiable();

            // Act
            var assertedException = await Assert.ThrowsAsync<ValidationException>(() =>
                 _commandHandler.ExecuteAsync(new ApproveProcessCommand
                 {
                     ProcessId = 1
                 }));

            // Assert
            _repository.VerifyAll();
            _repository.VerifyNoOtherCalls();
            assertedException.Id.Should().Be(1);
        }
    }
}
