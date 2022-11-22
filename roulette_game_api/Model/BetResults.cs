using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace roulette_game_api.Model
{
    public class BetResults 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Player { get; set; }
        public string Color { get; set; }  
        public int Rolled { get; set; }
        public double BetAmount { get; set; }
        public string BetType { get; set; }
        public string Status { get; set; }
    }
}
