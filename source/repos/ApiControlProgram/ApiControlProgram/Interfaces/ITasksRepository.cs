﻿using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface ITasksRepository
    {
        ICollection<Tasks> GetTasks();
        Tasks GetTask(int TaskId);
        Tasks GetTaskRegis(string NoRegis);
        bool TaskExist(int TaskId);
        bool CreateTask(Tasks tasks);
        bool UpdateTask(Tasks tasks);
        bool DeleteTask(Tasks tasks);
        bool Save();
    }
}
