using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.Core
{
    public class PersonRowConfig
    {
        public bool IsAgeRequired { get; set; } = true;
        public bool IsSexRequired { get; set; } = true;
        public bool IsPhoneNumberRequired { get; set; } = true;
        public bool IsActiveRequired { get; set; } = true;
        public bool IsFirstNameRequired { get; set; } = true;
        public bool IsLastNameRequired { get; set; } = true;
        
    }
}
