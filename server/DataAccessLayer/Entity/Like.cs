namespace DataAccessLayer.Entity
{
    public class Like : IEntity
    {
        public int Id { get; set ; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
