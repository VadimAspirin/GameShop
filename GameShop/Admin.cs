namespace GameShop
{
    public class Admin : Entity<int>, IUser
    {
        public Admin(int id) : base(id) { }
    }
}