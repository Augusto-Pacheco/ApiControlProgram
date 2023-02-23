using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProject(int ProjectId);
        Project GetProjectByName(string Name);
        bool ProjectExist(int ProjectId);
    }
}
