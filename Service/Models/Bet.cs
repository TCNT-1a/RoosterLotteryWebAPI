using System;
using System.Collections.Generic;

namespace Service.Models
{
    public partial class Bet
    {
        public Bet()
        {
            PlayerBets = new HashSet<PlayerBet>();
        }

        public int Id { get; set; }
        public DateTime DrawTime { get; set; }
        public int? ResultNumber { get; set; }

        public virtual ICollection<PlayerBet> PlayerBets { get; set; }
    }
}
