using ApiControlProgram.Data;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.Design;

namespace ApiControlProgram.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(ApplicationUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(ApplicationUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public ApplicationUser GetUser(string userName)
        {
            try
            {
                var user = _context.users.Where(p => p.UserName == userName).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("No se encontró a alguien registrado con el usuario proporcionado");
                }
                return user;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        //public ApplicationUser GetUserByName(string name)
        //{
            
        //}


        public ICollection<ApplicationUser> GetUsers()
        {
            try
            {
                return _context.users.OrderBy(p => p.Id).ToList();
            }
            catch (Exception)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public bool NameExist(string name)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.FirstName == name || u.LastName == name);
                return user != null;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExist(string userName)
        {
            return _context.users.Any(a => a.UserName == userName);
        }

        ICollection<ApplicationUser> IUserRepository.GetUserByName(string name)
        {
            try
            {
                var users = _context.Users.Where(u => u.FirstName == name || u.LastName == name).ToList();
                if (users == null || users.Count == 0)
                {
                    throw new Exception("No se encontró un usuario con el nombre o apellido proporcionado");
                }
                return users;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }
    }
}
