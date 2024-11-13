using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrontendMentor.Todo.Wpf.Helpers;
using FrontendMentor.Todo.Wpf.Models;
using FrontendMentor.Todo.Wpf.Services;
using FrontendMentor.Todo.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf
{
    partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newTodoText;

        [ObservableProperty]
        private ObservableCollection<TodoItemViewModel> todoItems;

        private readonly TodoContext dbContext;
        private readonly IFactory<Models.Todo, TodoItemViewModel> todoItemVMFactory;
        private readonly ITodoService todoService;

        public MainWindowViewModel(TodoContext dbContext,
            IFactory<Models.Todo, TodoItemViewModel> todoItemVMFactory,
            ITodoService todoService)
        {
            this.dbContext = dbContext;
            this.todoItemVMFactory = todoItemVMFactory;
            this.todoService = todoService;

            NewTodoText = "";

            TodoItems = new ObservableCollection<TodoItemViewModel>(
                dbContext.Todos.Select(t => todoItemVMFactory.Create(t)).ToList());
        }

        [RelayCommand]
        async Task AddTodoAsync()
        {
            if (NewTodoText == "") return;

            var newTodo = await todoService.CreateTodoAsync(NewTodoText);
            TodoItems.Add(todoItemVMFactory.Create(newTodo));

            NewTodoText = "";
        }
    }
}
