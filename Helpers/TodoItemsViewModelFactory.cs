using FrontendMentor.Todo.Wpf.Models;
using FrontendMentor.Todo.Wpf.Services;
using FrontendMentor.Todo.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.Helpers
{
    class TodoItemsViewModelFactory : IFactory<Models.Todo, TodoItemViewModel>
    {
        private readonly ITodoService todoService;

        public TodoItemsViewModelFactory(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        public TodoItemViewModel Create(Models.Todo model)
        {
            return new TodoItemViewModel(todoService, model);
        }
    }
}
