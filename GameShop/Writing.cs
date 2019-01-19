using System;

namespace GameShop
{
    public enum WritingType { Comment, News }
    
    public class Writing
    {
        public WritingType Type { get; }
        public string Text { get; }
        public DateTime CreaturaDate { get; }
        public IUser Auctor { get; }

        public Writing(IUser auctor, string text, WritingType type)
        {
            CreaturaDate = DateTime.Now;
            Text = text;
            Type = type;
            Auctor = auctor;
        }
    }
}