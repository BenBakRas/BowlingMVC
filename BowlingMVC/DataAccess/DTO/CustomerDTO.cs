﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessDTO;
using System.Threading.Tasks;

namespace DataAccessDTO.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

    }
}
