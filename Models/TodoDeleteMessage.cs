using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.Models
{
    class TodoDeleteMessage : AsyncRequestMessage<int>
    {
        public int TodoId { get; private set; }

        public TodoDeleteMessage(int id) 
        {
            TodoId = id;
        }
    }

    class TodoUpdateMessage : ValueChangedMessage<Todo>
    {
        public TodoUpdateMessage(Todo value) : base(value)
        {
        }
    }
}
