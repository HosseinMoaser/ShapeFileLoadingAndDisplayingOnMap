using Catfood.Shapefile;
using Microsoft.Maps.MapControl.WPF;
using ShapeFileLoading.App.Components;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ShapeFileLoading.App.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public HomeView()
        {
            InitializeComponent();
            LoadShapeFiles();
        }


        private void LoadShapeFiles()
        {
            foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "\\ShapeFiles\\", "*.shp"))
            {
                ShapeFileContainer shapeFileContainer = new ShapeFileContainer(new Shapefile(file));
                AddContainerToPanel(shapeFileContainer, file);
                ApplyContainerEvents(shapeFileContainer);
            }
        }

        // Add Layers To Layers List
        private void AddContainerToPanel(ShapeFileContainer container, string path)
        {
            LayersPanel.Children.Add(container);
            DockPanel.SetDock(container,Dock.Left);
            container.Margin = new Thickness(5);
            container.HorizontalAlignment = HorizontalAlignment.Left;
            container.txtLayerName.Text = System.IO.Path.GetFileName(path).Replace(".shp","");
        }

        // Apply Layers Events
        private void ApplyContainerEvents(ShapeFileContainer container)
        {
            container.MouseEnter += Container_MouseEnter;
            container.MouseLeave += Container_MouseLeave;
            container.MouseDown += Container_MouseDown;
        }

        // Layers Click Event
        private void Container_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShapeFileContainer container = sender as ShapeFileContainer;
            MapLayer mapLayer = new MapLayer();
            myMap.Children.Add(mapLayer);
            CreateVisibilityController(mapLayer, container);
        }

        // Layers Mouse Leave Event
        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            ShapeFileContainer container = sender as ShapeFileContainer;
            container.Background = Brushes.Transparent;
        }

        // Layers Mouse Enter Event
        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            ShapeFileContainer container = sender as ShapeFileContainer;
            container.Background = Brushes.Bisque;
        }

        private void CreateVisibilityController(MapLayer mapLayer, ShapeFileContainer shapeFileContainer)
        {
            ShapeFileVisibilityController shapeFileVisibilityController = new ShapeFileVisibilityController(shapeFileContainer, mapLayer);
            AddVisibilityControllerToVisibilityControllersPanel(shapeFileVisibilityController);
        }

        private void AddVisibilityControllerToVisibilityControllersPanel(ShapeFileVisibilityController shapeFileVisibilityController)
        {
            VisibilityControllersPanel.Children.Add(shapeFileVisibilityController);
            DockPanel.SetDock(shapeFileVisibilityController, Dock.Top);
            shapeFileVisibilityController.VerticalAlignment = VerticalAlignment.Top;
            shapeFileVisibilityController.Margin = new Thickness(5);
        }
    }
}
