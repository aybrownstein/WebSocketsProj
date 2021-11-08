using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;  

namespace WebSocketsProj.Data
{
   public class TaskItemsContextFactory: IDesignTimeDbContextFactory<TaskItemsContext>
    {
        public TaskItemsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}WebSocketsProj.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new TaskItemsContext(config.GetConnectionString("ConStr"));
        }
    }
}
