namespace GameShop
{
    public class GameService
    {
        IDeliveryService _delivery;
        public int DaysLimit { get; }
        public GameService(IDeliveryService delivery, int daysLimit)
        {
            _delivery = delivery;
            DaysLimit = daysLimit;
        }

        public void BuyGame(Player player, Game game)
        {
            var buy = new GettingGameInfo(player, game);
            player.BuyGame(buy);
            _delivery.DeliverGame(game, player);
        }
        
        public void ReturnGame(Player player, int gameId)
        {
            player.ReturnGame(gameId, DaysLimit);
        }
        public void GiftGame(Player player, Player donee, Game game)
        {
            var buy = new GettingGameInfo(player, game);
            player.PresentGame(buy, donee);
        }
    }
}