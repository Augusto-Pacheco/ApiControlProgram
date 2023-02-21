using ApiControlProgram.Data;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using System.ComponentModel.Design;

namespace ApiControlProgram.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool ProjectExist(int ProjectId)
        {
            return _context.projects.Any(p => p.ProjectId == ProjectId);
        }

        public Project GetProject(int ProjectId)
        {
            try
            {
                var company = _context.projects.Where(p => p.ProjectId == ProjectId).FirstOrDefault();
                if (company == null)
                {
                    throw new Exception("No se encontró un projecto con el ID proporcionado");
                }
                return company;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public ICollection<Project> GetProjects()
        {
            try
            {
                return _context.projects.OrderBy(p => p.ProjectId).ToList();
            }
            catch (Exception)
            {

                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }
    }
}
