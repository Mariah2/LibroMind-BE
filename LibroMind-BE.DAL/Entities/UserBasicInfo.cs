namespace LibroMind_BE.DAL.Entities
{
    public class UserBasicInfo
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
    }
}
