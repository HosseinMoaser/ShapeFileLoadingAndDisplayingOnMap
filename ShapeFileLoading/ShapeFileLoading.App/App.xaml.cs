using ShapeFileLoading.App.Views;
using ShapeFileLoading.Domain.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShapeFileLoading.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ShapeFileConvertersServices _shapeFileConverterServices;
        private HomeView _homeView;
        public App()
        {
            _shapeFileConverterServices = new ShapeFileConvertersServices();
            _homeView = new HomeView(_shapeFileConverterServices);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_homeView);
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
