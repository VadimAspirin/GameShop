using System;
using System.Collections.Generic;
using System.Linq;

namespace GameShop
{
    public class Player : Entity<int>, IUser
    {
        public List<GettingGameInfo> GameInfos { get; }

        public decimal Balance { get; set; }
        
        public Player(int id) : base(id)
        {
            GameInfos = new List<GettingGameInfo>();
        }

        public void BuyGame(GettingGameInfo newGettingGameInfo)
        {
            if (GameInfos.Any(buy => buy.Game.Equals(newGettingGameInfo.Game)))
            {
                throw new InvalidOperationException("Game already exist in User Account");
            }

            if (Balance <= newGettingGameInfo.Game.Price)
            {
                throw new InvalidOperationException("Balance less then Game Price");
            }

            GameInfos.Add(newGettingGameInfo);
            Balance -= newGettingGameInfo.Game.Price;            
        }

        public void ReturnGame(int gameId, int daysLimit)
        {
            var gettingGameInfo = GameInfos.Find(gameInfo => gameInfo.Game.Id == gameId);
            if (gettingGameInfo == null)
            {
                throw new InvalidOperationException();
            }

            if ((gettingGameInfo.Date - DateTime.Now).Days > daysLimit)
            {
                throw new InvalidOperationException();
            }
            if (gettingGameInfo.Payer.Id != Id)
            {
                gettingGameInfo.Payer.Balance += gettingGameInfo.Game.Price;
            }
            else
            {
                Balance += gettingGameInfo.Game.Price;
            }
           
            GameInfos.Remove(gettingGameInfo);
        }

        public void GetGift(GettingGameInfo newGettingGameInfo)
        {
            if (GameInfos.Any(buy => buy.Game.Equals(newGettingGameInfo.Game)))
            {
                throw new InvalidOperationException("Game already exist in User Account");
            }
            GameInfos.Add(newGettingGameInfo);
        }

        public void PresentGame(GettingGameInfo gettingGameInfo, Player donee)
        {
            if (Balance <= gettingGameInfo.Game.Price)
            {
                throw new InvalidOperationException("Balance less then Game Price");
            }
            donee.GetGift(gettingGameInfo);
            Balance -= gettingGameInfo.Game.Price;
        }
    }
}