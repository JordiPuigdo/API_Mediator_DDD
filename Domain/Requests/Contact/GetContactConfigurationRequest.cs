﻿using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.Contact
{
    public class GetContactConfigurationRequest
    {
        public ContactSettingsTypeDto ContactSettingsType { get; set; }
        public string? ContactId { get; set; }
    }
}
