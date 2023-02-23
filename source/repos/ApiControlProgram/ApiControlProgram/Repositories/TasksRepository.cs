﻿using ApiControlProgram.Data;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;

namespace ApiControlProgram.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly DataContext _context;

        public TasksRepository(DataContext context)
        {
            _context = context;
        }

        public Tasks GetTask(int TaskId)
        {
            try
            {
                var task = _context.tasks.Where(t => t.TaskId == TaskId).FirstOrDefault();
                if (task == null)
                {
                    throw new Exception("No se encontró una tarea con el ID proporcionado");
                }

                return task;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public Tasks GetTaskRegis(string NoRegis)
        {
            try
            {
                var task = _context.tasks.Where(t => t.NoRegis == NoRegis).FirstOrDefault();
                if (task == null)
                {
                    throw new Exception("No se encontró una tarea con el ID proporcionado");
                }

                return task;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public ICollection<Tasks> GetTasks()
        {
            try
            {
                //return _context.Companies.OrderBy(p => p.CompanyId).ToList();
                return _context.tasks.OrderBy(t => t.TaskId).ToList();
            }
            catch (Exception)
            {

                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public bool TaskExist(int TaskId)
        {
            return _context.tasks.Any(t => t.TaskId == TaskId);
        }
    }
}
