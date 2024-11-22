using FrontendMentor.Todo.Wpf.Helpers;
using FrontendMentor.Todo.Wpf.Models;
using FrontendMentor.Todo.Wpf.Services;
using FrontendMentor.Todo.Wpf.View;
using FrontendMentor.Todo.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Data;

namespace FrontendMentor.Todo.Wpf
{
    // CommmunityToolkit IoC reference:
    // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/ioc

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services  { get; set; }

        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<TodoContext>();

            services.AddTransient<ITodoService, TodoService>();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<IFactory<Models.Todo, TodoItemViewModel>, TodoItemsViewModelFactory>();
            
            services.RegisterView<TodoItemViewModel, TodoItemView>();

            return services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Window window;

            window = new MainWindow();
            window.DataContext = App.Current.Services.GetService<MainWindowViewModel>();

            window.Show();
        }
    }

    static class ServiceProviderExtensions
    {
        public static void RegisterView<Tvm, Tview>(this ServiceCollection services)
            where Tvm : class
            where Tview : FrameworkElement
        {
            services.AddScoped<Tvm>();

            var dataTemplate = new DataTemplate(typeof(Tvm));
            var factory = new FrameworkElementFactory(typeof(Tview));
            factory.SetBinding(FrameworkElement.DataContextProperty, new Binding());

            dataTemplate.VisualTree = factory;

            App.Current.Resources.Add(new DataTemplateKey(typeof(Tvm)), dataTemplate);
        }

        public static void RegisterView(this ServiceCollection services, Type vm, Type view)
        {
            services.AddScoped(vm);

            var dataTemplate = new DataTemplate(view);
            var factory = new FrameworkElementFactory(view);
            factory.SetBinding(FrameworkElement.DataContextProperty, new Binding());

            dataTemplate.VisualTree = factory;
            App.Current.Resources.Add(new DataTemplateKey(vm), dataTemplate);
        }
    }
}
