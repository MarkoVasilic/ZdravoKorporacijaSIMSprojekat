﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.View.SecretaryUI.DTO
{
    public class UserDetailsDto
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String JMBG { get; set; }

        public UserDetailsDto(String firstName, String lastName, String jmbg)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.JMBG = jmbg;
        }
    }
}
