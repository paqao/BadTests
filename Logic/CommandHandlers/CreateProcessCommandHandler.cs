using AutoMapper;
using Logic.Commands;
using Logic.DTOs;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CommandHandlers
{
    public class CreateProcessCommandHandler : ICommandHandler<CreateProcessCommand, BusinessProcessDto>
    {
        private readonly IRepository<BusinessProcess> _businessProcessRepository;
        private readonly IMapper _mapper;

        public CreateProcessCommandHandler(IRepository<BusinessProcess> businessProcessRepository, IMapper mapper)
        {
            _businessProcessRepository = businessProcessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessProcessDto> ExecuteAsync(CreateProcessCommand command)
        {
            var item = _mapper.Map<BusinessProcess>(command);

            item.CreateDate = DateTime.Now;
            item.UpdateDate = DateTime.Now;
            item.Status = Const.BusinessProcessConstants.Status.New;

            item = await _businessProcessRepository.Create(item);

            return _mapper.Map<BusinessProcessDto>(item);
        }
    }
}
