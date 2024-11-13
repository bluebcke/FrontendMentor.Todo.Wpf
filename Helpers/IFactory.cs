using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.Helpers
{
    interface IFactory<TModel, TViewModel>
    {
        TViewModel Create(TModel model);
    }
}
