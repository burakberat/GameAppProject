using System.Text.Json.Serialization;

namespace GameApp.Model.Dtos.UserDtos
{
    public class UserRegisterDto: BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EPosta { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
