﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class BoardBet
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int? BetID { get; set; }
        public DateTime? DrawTime { get; set; }
        public int? BetNumber { get; set; }
        public int? ResultNumber { get; set; }
        public int? IsWinner { get; set; }
    }
}
