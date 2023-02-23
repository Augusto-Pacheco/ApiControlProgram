using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface IExtrasRepository
    {
        ICollection<Types> GetTypes();
        ICollection<Categories> GetCategories();
    }
}
