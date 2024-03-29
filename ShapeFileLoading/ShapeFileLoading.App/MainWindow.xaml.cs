﻿using ShapeFileLoading.App.Views;
using ShapeFileLoading.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShapeFileLoading.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HomeView _homeView;
        public MainWindow(HomeView homeView)
        {
            InitializeComponent();
            _homeView = homeView;
            HomeViewGrid.Children.Add(_homeView);
        }
    }
}
