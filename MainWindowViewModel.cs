using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    partial class MainWindowViewModel : ObservableObject,
        IRecipient<TodoDeleteMessage>,
        IRecipient<TodoUpdateMessage>
    {
        [ObservableProperty]
        private string newTodoText;

        [ObservableProperty]
        private ObservableCollection<TodoItemViewModel> todoItems;

        private readonly IFactory<Models.Todo, TodoItemViewModel> todoItemVMFactory;
        private readonly ITodoService todoService;

        public MainWindowViewModel(
            IFactory<Models.Todo, TodoItemViewModel> todoItemVMFactory,
            ITodoService todoService)
        {
            this.todoItemVMFactory = todoItemVMFactory;
            this.todoService = todoService;

            NewTodoText = "";

            TodoItems = new ObservableCollection<TodoItemViewModel>(
                todoService.GetTodos().Select(t => todoItemVMFactory.Create(t)).ToList());

            WeakReferenceMessenger.Default.Register<TodoDeleteMessage>(this);
            WeakReferenceMessenger.Default.Register<TodoUpdateMessage>(this);
        }

        [RelayCommand]
        async Task AddTodoAsync()
        {
            if (NewTodoText == "") return;

            var newTodo = await todoService.CreateTodoAsync(NewTodoText);
            TodoItems.Add(todoItemVMFactory.Create(newTodo));

            NewTodoText = "";
        }


        public async void Receive(TodoDeleteMessage message)
        {
            await todoService.DeleteTodoAsync(message.TodoId);

            var todoRemove = TodoItems.FirstOrDefault(vm => vm.MatchesId(message.TodoId));
            if (todoRemove != null) 
            {
                TodoItems.Remove(todoRemove);
            }
        }

        public void Receive(TodoUpdateMessage message)
        {
            todoService.UpdateAsync(message.Value);
        }
    }
}
