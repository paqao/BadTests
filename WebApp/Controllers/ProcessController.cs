using Logic.CommandHandlers;
using Logic.Commands;
using Logic.DTOs;
using Logic.Queries;
using Logic.QueryHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/processes")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private ICommandHandler<ApproveProcessCommand, BusinessProcessDto> _approveProcessCommandHandler;
        private ICommandHandler<CreateProcessCommand, BusinessProcessDto> _createProcessCommandHandler;
        private ICommandHandler<UpdateProcessCommand, BusinessProcessDto> _updateProcessCommandHandler;
        private IQueryHandler<GetItemById, BusinessProcessDto> _getItemById;

        public ProcessController(ICommandHandler<ApproveProcessCommand, BusinessProcessDto> approveProcessCommandHandler, ICommandHandler<CreateProcessCommand, BusinessProcessDto> createProcessCommandHandler,
            IQueryHandler<GetItemById, BusinessProcessDto> getItemById, ICommandHandler<UpdateProcessCommand, BusinessProcessDto> updateProcessCommandHandler)
        {
            _approveProcessCommandHandler = approveProcessCommandHandler;
            _createProcessCommandHandler = createProcessCommandHandler;
            _getItemById = getItemById;
            _updateProcessCommandHandler = updateProcessCommandHandler;
        }

        [HttpPost("{processId:int}/approve")]
        public async Task<BusinessProcessDto> ApproveBusinessProcessAsync(int processId)
        {
            return await _approveProcessCommandHandler.ExecuteAsync(new ApproveProcessCommand
            {
                ProcessId = processId
            });
        }

        [HttpPost()]
        public async Task<BusinessProcessDto> CreateBusinessProcessAsync([FromBody] CreateProcessDto data)
        {
            return await _createProcessCommandHandler.ExecuteAsync(new CreateProcessCommand
            {
                Name = data.Name,
                Author = data.Author,
                Comment = data.Comment,
                Division = data.Division
            });
        }

        [HttpGet("{processId:int}")]
        public async Task<BusinessProcessDto> GetBusinessProcessById(int processId)
        {
            return await _getItemById.Execute(new GetItemById
            {
                Id = processId
            });
        }

        [HttpPut("{processId:int}")]
        public async Task<BusinessProcessDto> UpdateProcessAsync(int processId, [FromBody] UpdateProcessDto processData)
        {
            return await _updateProcessCommandHandler.ExecuteAsync(new UpdateProcessCommand
            {
                Id = processId,
                Name = processData.Name,
                Author = processData.Author,
                Comment = processData.Comment,
                Division = processData.Division
            });
        }
    }
}
