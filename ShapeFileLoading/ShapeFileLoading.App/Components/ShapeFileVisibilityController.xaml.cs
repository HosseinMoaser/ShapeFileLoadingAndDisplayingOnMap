using Microsoft.Maps.MapControl.WPF;
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

namespace ShapeFileLoading.App.Components
{
    /// <summary>
    /// Interaction logic for ShapeFileVisibilityController.xaml
    /// </summary>
    public partial class ShapeFileVisibilityController : UserControl
    {
        private MapLayer _mapLayer;
        private ShapeFileContainer _shapeFileContainer;
        public ShapeFileVisibilityController(ShapeFileContainer shapeFileContainer,MapLayer mapLayer)
        {
            InitializeComponent();
            _shapeFileContainer = shapeFileContainer;
            _mapLayer = mapLayer;
            txbLayerName.Text = shapeFileContainer.txtLayerName.Text;
        }

        private void cbxContainerVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (cbxContainerVisibility.IsChecked == true)
                _mapLayer.Visibility = Visibility.Visible;
            else
                _mapLayer.Visibility = Visibility.Collapsed;
        }
    }
}
