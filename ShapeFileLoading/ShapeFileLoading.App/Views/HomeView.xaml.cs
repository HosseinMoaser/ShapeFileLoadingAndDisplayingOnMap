using Catfood.Shapefile;
using Microsoft.Maps.MapControl.WPF;
using ShapeFileLoading.App.Components;
using ShapeFileLoading.Domain.ExtensionMethod;
using ShapeFileLoading.Domain.Services;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShapeFileLoading.App.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private ShapeFileConvertersServices _shapeFileConvertersServices;
        public HomeView(ShapeFileConvertersServices shapeFileConvertersServices)
        {
            InitializeComponent();
            LoadShapeFiles();
            _shapeFileConvertersServices = shapeFileConvertersServices;
        }

        // Load Shape Files In Shape File Folder
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
            DockPanel.SetDock(container, Dock.Left);
            container.Margin = new Thickness(5);
            container.HorizontalAlignment = HorizontalAlignment.Left;
            container.txtLayerName.Text = System.IO.Path.GetFileName(path).Replace(".shp", "");
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
            
            if(container.IsEnable)
            {
                MapLayer mapLayer = new MapLayer();
                myMap.Children.Add(mapLayer);
                CreateVisibilityController(mapLayer, container);
                foreach (Shape shape in container.ShapeFile)
                {
                    RenderLayerOnMap(shape, mapLayer);
                }

                container.IsEnable = false;
            }
            
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

        // Create A Visibility Controller
        private void CreateVisibilityController(MapLayer mapLayer, ShapeFileContainer shapeFileContainer)
        {
            ShapeFileVisibilityController shapeFileVisibilityController = new ShapeFileVisibilityController(shapeFileContainer, mapLayer);
            AddVisibilityControllerToVisibilityControllersPanel(shapeFileVisibilityController);
        }

        // Add Visibility Controller To Corresspond Panel
        private void AddVisibilityControllerToVisibilityControllersPanel(ShapeFileVisibilityController shapeFileVisibilityController)
        {
            VisibilityControllersPanel.Children.Add(shapeFileVisibilityController);
            DockPanel.SetDock(shapeFileVisibilityController, Dock.Top);
            shapeFileVisibilityController.VerticalAlignment = VerticalAlignment.Top;
            shapeFileVisibilityController.Margin = new Thickness(5);
        }

        // Display Layer On Map
        private void RenderLayerOnMap(Shape shape, MapLayer mapLayer)
        {
            if(shape.Type == ShapeType.Polygon)
            {
                ShapePolygon polygon = shape as ShapePolygon;
                for (int i = 0; i < polygon.Parts.Count; i++)
                {
                    if (!polygon.Parts[i].IsCCW())
                    {
                        mapLayer.Children.Add(new MapPolygon()
                        {
                            Locations = _shapeFileConvertersServices.PointDArrayToLocationCollection(polygon.Parts[i]),
                            Fill = new SolidColorBrush(Color.FromArgb(150, 0, 0, 255)),
                            Opacity = 0.7,
                            Stroke = new SolidColorBrush(Color.FromArgb(150, 255, 0, 0))
                        });
                    }
                }
            }
        }
    }
}
