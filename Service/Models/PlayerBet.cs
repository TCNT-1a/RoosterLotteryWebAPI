using System;
using System.Collections.Generic;

namespace Service.Models
{
    public partial class PlayerBet
    {
        public int Id { get; set; }
        public int? BetId { get; set; }
        public int? PlayerId { get; set; }
        public int? BetNumber { get; set; }
        public bool? IsWinner { get; set; }

        public virtual Bet? Bet { get; set; }
        public virtual Player? Player { get; set; }
    }
}
