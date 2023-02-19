using AutoMapper;
using Logic.Commands;
using Logic.DTOs;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Profiles
{
    public class BusinessProcessProfile : Profile
    {
        public BusinessProcessProfile()
        {
            CreateMap<UpdateProcessCommand, BusinessProcess>()
                .ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BusinessProcess, BusinessProcessDto>();
            CreateMap<CreateProcessCommand, BusinessProcess>();
        }
    }
}
