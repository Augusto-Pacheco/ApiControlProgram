using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string userName);
        ICollection<ApplicationUser> GetUserByName(string name);
        bool UserExist(string userName);
        bool NameExist(string name);
        bool CreateUser(ApplicationUser user);
        bool UpdateUser(ApplicationUser user);
        bool DeleteUser(ApplicationUser user);
        bool Save();
    }
}
