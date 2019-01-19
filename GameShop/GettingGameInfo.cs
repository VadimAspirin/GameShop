using System;

namespace GameShop
{
    public class GettingGameInfo
    {
        public DateTime Date { get; }
        public Game Game { get; }
        public Player Payer { get; }

        public GettingGameInfo(Player payer, Game game)
        {
            Payer = payer;
            Game = game;
            Date = DateTime.Now;
        }
    }
}