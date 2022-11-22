namespace roulette_game_api.Model
{
    public class PlaceBet
    {
        public int Id { get; set; }
        public string BetType { get; set; }
        public string PlayerName { get; set; }
        public int Bet { get; set; } 
        public double Amount { get; set; }  
    }
}
