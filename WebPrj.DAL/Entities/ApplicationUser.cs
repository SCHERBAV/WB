using Microsoft.AspNetCore.Identity;

namespace WebPrj.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //lb5. Для возможности хранения аватарки пользователя
        public byte[] AvatarImage { get; set; }     //массив байтов, содержащий аватарку пользователя
    }
}