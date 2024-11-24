using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FrontendMentor.Todo.Wpf.Models;
using FrontendMentor.Todo.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.ViewModels
{
    partial class TodoItemViewModel : ObservableObject
    {
        internal bool MatchesId(int id) => model.Id == id;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private bool isCompleted;

        private Models.Todo model;
        private readonly ITodoService todoService;

        public TodoItemViewModel(
            ITodoService todoService,
            Models.Todo model)
        {
            this.model = model;
            this.todoService = todoService;

            Title = model.Title;
            IsCompleted = model.Completed;
        }

        partial void OnTitleChanged(string value)
        {
            model.Title = value;
            WeakReferenceMessenger.Default.Send(new TodoUpdateMessage(model));
        }

        partial void OnIsCompletedChanged(bool value)
        {
            model.Completed = value;
            WeakReferenceMessenger.Default.Send(new TodoUpdateMessage(model));
        }

        [RelayCommand]
        private void Delete()
        {
            WeakReferenceMessenger.Default.Send(new TodoDeleteMessage(model.Id));
        }
    }

}
