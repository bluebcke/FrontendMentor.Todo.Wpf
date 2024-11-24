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
        IEnumerable<Models.Todo> GetTodos();
        Task DeleteTodoAsync(int id);
        Task UpdateAsync(Models.Todo todo);
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

        public async Task DeleteTodoAsync(int id)
        {
            var todo = dbContext.Todos.Single(m => m.Id == id);
            dbContext.Todos.Remove(todo);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<Models.Todo> GetTodos() { return dbContext.Todos; }

        public async Task UpdateAsync(Models.Todo newTodo)
        {
            var todo = await dbContext.Todos.FindAsync(newTodo.Id)
                ?? throw new KeyNotFoundException($"Todo with id {newTodo.Id} not found");

            dbContext.Entry(todo).CurrentValues.SetValues(newTodo);
            await dbContext.SaveChangesAsync();
        }
    }
}
