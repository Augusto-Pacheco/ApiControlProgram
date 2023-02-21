﻿using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProject(int ProjectId);
        bool ProjectExist(int ProjectId);
    }
}