using ApiControlProgram.Data;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;

namespace ApiControlProgram.Repositories
{
    public class ExtrasRepository : IExtrasRepository
    {
        private readonly DataContext _context;

        public ExtrasRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Categories> GetCategories()
        {
            try
            {
                return _context.Categories.OrderBy(p => p.CategoryId).ToList();
            }
            catch (Exception)
            {

                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public ICollection<Types> GetTypes()
        {
            try
            {
                return _context.types.OrderBy(p => p.TypeId).ToList();
            }
            catch (Exception)
            {
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }
    }
}
