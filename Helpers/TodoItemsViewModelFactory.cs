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
        public TodoItemsViewModelFactory(/* inject other dependencies if needed */)
        {
            
        }

        public TodoItemViewModel Create(Models.Todo model)
        {
            return new TodoItemViewModel(model);
        }
    }
}
