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
    public class UpdateProcessCommandHandler : ICommandHandler<UpdateProcessCommand, BusinessProcessDto>
    {
        private readonly IRepository<BusinessProcess> _businessProcessRepository;
        private readonly IMapper _mapper;

        public UpdateProcessCommandHandler(IRepository<BusinessProcess> businessProcessRepository, IMapper mapper)
        {
            _businessProcessRepository = businessProcessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessProcessDto> ExecuteAsync(UpdateProcessCommand command)
        {
            var item = await _businessProcessRepository.GetById(command.Id);
            if (item == null)
            {
                throw new NotFoundException(typeof(BusinessProcess).Name, command.Id);
            }

            if (item.Status == Const.BusinessProcessConstants.Status.Closed)
            {
                throw new ValidationException(typeof(BusinessProcess).Name, command.Id, "Process should have status different to closed");
            }

            item = _mapper.Map(command, item);
            item.UpdateDate = DateTime.Now;
            item.Status = Const.BusinessProcessConstants.Status.OutOfDate;

            await _businessProcessRepository.Update(item);

            return _mapper.Map<BusinessProcessDto>(item);
        }
    }
}
