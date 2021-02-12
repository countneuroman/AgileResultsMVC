using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace AgileResultsMVC.Models
{
    //Данная модель используется для аутентификации пользователя.
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Задачи пользователя
        public List<AllTask> AllTask { get; set; }
    }
}
