using Microsoft.AspNetCore.Identity;

namespace AgileResultsMVC.Models
{
    //Данная модель используется для аутентификации пользователя.
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Задачи пользователя
        public AllTask AllTask { get; set; }
    }
}
