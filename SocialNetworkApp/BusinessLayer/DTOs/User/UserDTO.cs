namespace BusinessLayer.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
