using System;

namespace GameShop
{
    public class Payment
    {
        public DateTime data { get; }
        public Player Player { get; }
        public decimal Amount { get; }

        public Payment(Player player, decimal amount)
        {
            Player = player;
            Amount = amount;
            data = DateTime.Now;
        }
    }
}