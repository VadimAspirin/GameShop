using System;
using System.Collections.Generic;
using System.Linq;

namespace GameShop
{
    public class Game : Entity<int>
    {
        private List<Writing> _writings;
        
        public string Name { get; set; }

        public string LinkToFile { get; set; }

        public decimal Price { get; set; }

        public Game(int id, string name, decimal price, string link) : base(id)
        {
            Name = name;
            LinkToFile = link;
            Price = price;
            _writings = new List<Writing>();
        }
        public void CommentGame(string comment, Player player)
        {
            var newComment = new Writing(player, comment, WritingType.Comment);
            _writings.Add(newComment);
        }
        public void AddNews(string content, IUser admin)
        {
            if (!(admin is Admin))
            {
                throw new MemberAccessException();
            }
            var news = new Writing(admin, content, WritingType.News);
            _writings.Add(news);
        }

        public List<Writing> GetComments()
        {
            return _writings.FindAll(writing => writing.Type == WritingType.Comment);
        }
        
        public List<Writing> GetNews()
        {
            return _writings.FindAll(writing => writing.Type == WritingType.News);
        }
    }
}