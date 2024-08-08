using AutoMapper;
using Database.Models;
using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        { 
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<ContactDto, Contact>();
            CreateMap<Contact, ContactDto>();
            CreateMap<ExtraInformation, ExtraInformationDto>();
            CreateMap<ExtraInformationDto, ExtraInformation>();
            CreateMap<ContactConfiguration, ContactConfigurationDto>();
            CreateMap<ContactConfigurationDto, ContactConfiguration>();
            CreateMap<ContactSettings, ContactSettingsDto>();
            CreateMap<ContactSettingsDto, ContactSettings>();
            CreateMap<Account, AccountDto>(); 
            CreateMap<AccountDto, Account>();


        }
    }
}
