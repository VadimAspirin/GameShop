using System;
using NUnit.Framework;

namespace GameShop
{
    [TestFixture]
    public class Game_should
    {
        [Test]
        public static void SuccessfulComment()
        {
            var player = new Player(1);
            var game = new Game(1, "Тетрис", 125, "ftp://werty");
            var comment = "Ужасная игра";
            game.CommentGame(comment, player);           
            var comments = game.GetComments()
                               .FindAll(writing => writing.Auctor.Equals(player));
            Assert.AreNotEqual(comments.Count, 0);
            Assert.NotNull(comments.Find(writing => writing.Text == comment));
        }
        
        [Test]
        public static void NoAccessToAddingNews()
        {
            var player = new Player(1);
            var game = new Game(1, "Тетрис", 125, "ftp://werty");
            var newsText = "Новая версия";
            
            Assert.Throws<MemberAccessException>(() => game.AddNews(newsText, player));
        }
        
        [Test]
        public static void SuccessfulAddingNews()
        {
            var admin = new Admin(1);
            var game = new Game(1, "Тетрис", 125, "ftp://werty");
            var newsText = "Новая версия";
            game.AddNews(newsText, admin);
            var news = game.GetNews()
                           .FindAll(writing => writing.Auctor.Equals(admin));
            Assert.AreNotEqual(news.Count, 0);
            Assert.NotNull(news.Find(writing => writing.Text == newsText));
        }
        
        // Зачисление средств на счет Игрока
        // Вывод средств со счета Игрока
    }
}