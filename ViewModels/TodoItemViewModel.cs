using CommunityToolkit.Mvvm.ComponentModel;
using FrontendMentor.Todo.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.ViewModels
{
    partial class TodoItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private bool isCompleted;

        private Models.Todo model;

        public TodoItemViewModel(Models.Todo model)
        {
            this.model = model;

            Title = model.Title;
            IsCompleted = model.Completed;
        }

        partial void OnTitleChanged(string value)
        {
            model.Title = value;
        }

        partial void OnIsCompletedChanged(bool value)
        {
            model.Completed = value;
        }
    }

}
