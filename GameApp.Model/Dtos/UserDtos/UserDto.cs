namespace GameApp.Model.Dtos.UserDtos
{
    public class UserDto: BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EPosta { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
