using AutoMapper;
using Logic.Commands;
using Logic.DTOs;
using Logic.Exceptions;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CommandHandlers
{
    public class ApproveProcessCommandHandler : ICommandHandler<ApproveProcessCommand, BusinessProcessDto>
    {
        private readonly IRepository<BusinessProcess> _businessProcessRepository;
        private readonly IMapper _mapper;

        public ApproveProcessCommandHandler(IRepository<BusinessProcess> businessProcessRepository, IMapper mapper)
        {
            _businessProcessRepository = businessProcessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessProcessDto> ExecuteAsync(ApproveProcessCommand command)
        {
            var item = await _businessProcessRepository.GetById(command.ProcessId);
            if (item == null)
            {
                throw new NotFoundException(typeof(BusinessProcess).Name, command.ProcessId);
            }

            if (item.Status != Const.BusinessProcessConstants.Status.New && item.Status != Const.BusinessProcessConstants.Status.OutOfDate)
            {
                throw new ValidationException(typeof(BusinessProcess).Name, command.ProcessId, "Process should have new status");
            }

            item.UpdateDate = DateTime.Now;
            item.Status = Const.BusinessProcessConstants.Status.Approved;

            await _businessProcessRepository.Update(item);

            return _mapper.Map<BusinessProcessDto>(item);
        }
    }
}
