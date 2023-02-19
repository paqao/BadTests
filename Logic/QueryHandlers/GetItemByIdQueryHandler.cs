using AutoMapper;
using Logic.DTOs;
using Logic.Exceptions;
using Logic.Models;
using Logic.Queries;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.QueryHandlers
{
    public class GetItemByIdQueryHandler : IQueryHandler<GetItemById, BusinessProcessDto>
    {
        private readonly IRepository<BusinessProcess> _businessProcessRepository;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler(IRepository<BusinessProcess> repository, IMapper mapper)
        {
            _businessProcessRepository = repository;
            _mapper = mapper;
        }

        public async Task<BusinessProcessDto> Execute(GetItemById query)
        {
            var item = await _businessProcessRepository.GetById(query.Id);
            if (item == null)
            {
                throw new NotFoundException(typeof(BusinessProcess).Name, query.Id);
            }

            return _mapper.Map<BusinessProcessDto>(item);
        }
    }
}
