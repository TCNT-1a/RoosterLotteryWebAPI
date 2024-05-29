using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerBets = new HashSet<PlayerBet>();
        }

        
        [JsonIgnore]
        public int Id { get; set; }
       
        public string FullName { get; set; } = null!;
       
        public DateTime? DateOfBirth { get; set; }
        
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlayerBet> PlayerBets { get; set; }
    }
}
