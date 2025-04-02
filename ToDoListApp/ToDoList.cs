using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp
{
    internal class ToDoList
    {
        private readonly List<TaskItem> _tasks;
        private int _nextId = 1;

        public ToDoList()
        {
            _tasks = new List<TaskItem>();
        }

        public void AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Описание задачи не может быть пустым");

            _tasks.Add(new TaskItem(_nextId++, description));
        }

        public bool CompleteTask(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) return false;

            task.IsCompleted = true;
            return true;
        }

        public bool RemoveTask(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) return false;

            return _tasks.Remove(task);
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _tasks.OrderBy(t => t.Id);
        }

        public IEnumerable<TaskItem> GetCompletedTasks()
        {
            return _tasks.Where(t => t.IsCompleted).OrderBy(t => t.Id);
        }

        public IEnumerable<TaskItem> GetPendingTasks()
        {
            return _tasks.Where(t => !t.IsCompleted).OrderBy(t => t.Id);
        }

        public void Clear()
        {
            _tasks.Clear();
            _nextId = 1;
        }
    }
}
