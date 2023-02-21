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
    public class UpdteProcessCommandHandlerTests
    {
        private readonly UpdateProcessCommandHandler _commandHandler;
        private readonly Mock<IRepository<BusinessProcess>> _repository;

        public UpdteProcessCommandHandlerTests()
        {
            _repository = new Mock<IRepository<BusinessProcess>>();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BusinessProcessProfile>();
            });

            _commandHandler = new UpdateProcessCommandHandler(_repository.Object, mapper.CreateMapper());
        }

        [Fact]
        public async Task When_BusinessProcessExist_Then_WeMergeStoredAndProvidedObject()
        {
            // Arrange 
            _repository
                .Setup(s => s.GetById(1))
                .ReturnsAsync(new BusinessProcess
                {
                    Id = 1,
                    Status = BusinessProcessConstants.Status.New,
                    Division = "IT",
                    Author = "PL",
                    UpdateDate = new DateTime(2023, 2, 21, 4, 0, 0)
                })
                .Verifiable();

            BusinessProcess storedBusinessProcess = null;
            _repository
                .Setup(s => s.Update(It.IsAny<BusinessProcess>()))
                .Callback((BusinessProcess bussinessProcess) => storedBusinessProcess = bussinessProcess);

            // Act
            await _commandHandler.ExecuteAsync(new UpdateProcessCommand
            {
                Id = 1,
                Author = "MB"
            });

            // Assert
            storedBusinessProcess.Id.Should().Be(1);
            storedBusinessProcess.Division.Should().Be("IT");
            storedBusinessProcess.Author.Should().Be("MB");
        }
    }
}
