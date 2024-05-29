using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.Models;

using System.ComponentModel.DataAnnotations;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly RoosterLotteryContext _context;
        public PlayerController(RoosterLotteryContext context)
        {
            this._context = context;
        }

        [HttpPost("findPlayer")]

        public List<Player> findPlayerByPhoneNumber([FromBody]  SearchPlayer s)
        {
            var phoneNumber = s.phoneNumber!=null?s.phoneNumber:"";

            var phoneNumberParameter = new SqlParameter("@PhoneNumber", phoneNumber);

            var p = _context.Players
                .FromSqlRaw("EXEC dbo.FindPlayerByPhoneNumber @PhoneNumber"
                , phoneNumberParameter).ToList();

            return p;
        
        }
        [HttpPost("createPlayer")]

        public int createPlayer([FromBody] Player p)
        {
            Console.WriteLine(p);

            var fullNameParas = new SqlParameter("@FullName", p.FullName);
            var d = String.Format("{0:dd-MM-yyyy}", p.DateOfBirth);
            var dateOfBirthParas = new SqlParameter("@DateOfBirth", d);
            var phoneParas = new SqlParameter("@PhoneNumber", p.PhoneNumber);

             var c = _context.Players
                .FromSqlRaw("EXEC dbo.CreatePlayer @FullName @DateOfBirth @PhoneNumber"
                , fullNameParas, dateOfBirthParas, phoneParas).ToList();
            Console.WriteLine(c);
            //return p;
            return 1;

        }
    }
    public class SearchPlayer
    {
        [MaxLength (15)]
        
        public string? phoneNumber { get; set; }
    }
}
