using FrontendMentor.Todo.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.Services
{
    interface ITodoService
    {
        Task<Models.Todo> CreateTodoAsync(string title);
    }

    class TodoService : ITodoService
    {
        private readonly TodoContext dbContext;

        public TodoService(TodoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Models.Todo> CreateTodoAsync(string title)
        {
            var todo = new Models.Todo
            {
                Title = title,
                Completed = false
            };

            dbContext.Todos.Add(todo);

            await dbContext.SaveChangesAsync();

            return todo;
        }
    }
}
