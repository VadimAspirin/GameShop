using System;
using Moq;
using NUnit.Framework;

namespace GameShop
{
    [TestFixture]
    public class Player_should
    {
        private static Mock<IDeliveryService> mock;

        public Player_should()
        {
            mock = new Mock<IDeliveryService>();
            mock.Setup(x => x.DeliverGame(It.IsAny<Game>(), It.IsAny<Player>()));
        }
        
        [Test]
        public static void SuccessfulPurchase()
        {
            var player = new Player(1) { Balance = 400 };
            var game = new Game(1, "Танчики", 300, "http://figa");
            
            var service = new GameService(mock.Object, 10);
            service.BuyGame(player, game);
            Assert.AreEqual(100, player.Balance);
            Assert.NotNull(player.GameInfos.Find(gameInfo => gameInfo.Game.Equals(game))); 
        }
        
        [Test]
        public static void NotEnoughMoneyToPay()
        {
            var player = new Player(1) { Balance = 100 };
            var game = new Game(1, "Танчики", 300, "http://figa");
            
            var service = new GameService(mock.Object, 10);
            Assert.Throws<InvalidOperationException>(() => service.BuyGame(player, game));
        }
        
        [Test]
        public static void GameAlreadyExists()
        {
            var player = new Player(1) { Balance = 5000 };
            var game = new Game(1, "Танчики", 300, "http://figa");
            
            var service = new GameService(mock.Object, 10);
            service.BuyGame(player, game);
            
            Assert.Throws<InvalidOperationException>(() => service.BuyGame(player, game));
        }

        [Test]
        public static void SuccessfulReturn()
        {
            var player = new Player(1) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
                     
            var service = new GameService(mock.Object, 10);
            service.BuyGame(player, game);
                      
            service.ReturnGame(player, 1);            
            Assert.AreEqual(500, player.Balance);
            Assert.IsNull(player.GameInfos.Find(gameInfo => gameInfo.Game.Equals(game))); 
        }
        
        [Test]
        public static void SuccessfulReturnByDonee()
        {
            var player = new Player(1) { Balance = 500 };
            var donee = new Player(2) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
                     
            var service = new GameService(mock.Object, 10);
            service.GiftGame(player, donee, game);
            service.ReturnGame(donee, 1);
                       
            Assert.AreEqual(500, player.Balance);
        }

        [Test]
        public static void ReturnTimeExpired()
        {
            var player = new Player(1) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
                     
            var service = new GameService(mock.Object, 10);
            service.BuyGame(player, game);

            Assert.Throws<InvalidOperationException>(() => service.ReturnGame(player, 0));
        }
        
        [Test]
        public static void SuccessfulGift()
        {
            var player = new Player(1) { Balance = 500 };
            var donee = new Player(2) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
                     
            var service = new GameService(mock.Object, 10);
            service.GiftGame(player, donee, game);
                    
            Assert.AreEqual(500, donee.Balance);
            Assert.AreEqual(200, player.Balance); 
            Assert.NotNull(donee.GameInfos.Find(gameInfo => gameInfo.Game.Equals(game))); 
        }
        
        [Test]
        public static void AlreadyExistsGift()
        {
            var player = new Player(1) { Balance = 500 };
            var donee = new Player(2) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
            
            var service = new GameService(mock.Object, 10);  
            service.BuyGame(donee, game);           
            Assert.Throws<InvalidOperationException>(() => service.GiftGame(player, donee, game));
            Assert.AreEqual(500, player.Balance);
            Assert.AreEqual(1, donee.GameInfos.FindAll(gameInfo => gameInfo.Game.Equals(game)).Count);
        }

        [Test]
        public static void NotEnoughMoneyToGift()
        {
            var player = new Player(1) { Balance = 100 };
            var donee = new Player(2) { Balance = 500 };
            var game = new Game(1, "Танчики", 300, "http://figa");
                     
            var service = new GameService(mock.Object, 10);
            Assert.Throws<InvalidOperationException>(() => service.GiftGame(player, donee, game));
            Assert.AreEqual(100, player.Balance);
            Assert.IsNull(donee.GameInfos.Find(gameInfo => gameInfo.Game.Equals(game)));
        }
    }
}