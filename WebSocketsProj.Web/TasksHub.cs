using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using WebSocketsProj.Data;

namespace WebSocketsProj.Web
{
    public class TasksHub: Hub
    {
        private readonly string _connectionString;

        public TasksHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public void NewTask(string title)
        {
            var taskrepo = new TasksRepository(_connectionString);
            var task = new TaskItem { Title = title, IsCompleted = false };
            taskrepo.AddTask(task);
            SendTasks();
        }

        private void SendTasks()
        {
            var taskrepo = new TasksRepository(_connectionString);
            var tasks = taskrepo.GetActiveTasks();

            Clients.All.SendAsync("RenderTasks", tasks.Select(t => new
            {
                Id = t.Id,
                Title = t.Title,
                HandledBy = t.HandledBy,
                UserDoingIt = t.User != null ? $"{t.User.FirstName} {t.User.LastName}" : null
            }));
        }

        public void GetAll()
        {
            SendTasks();
        }

        public void SetDoing(int taskId)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.GetByEmail(Context.User.Identity.Name);
            var taskrepo = new TasksRepository(_connectionString);
            taskrepo.SetDoing(taskId, user.Id);
            SendTasks();
        }

        public void SetDone(int taskId)
        {
            var taskrepo = new TasksRepository(_connectionString);
            taskrepo.SetCompleted(taskId);
            SendTasks();
        }
    }
}
