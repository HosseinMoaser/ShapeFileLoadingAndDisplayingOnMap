using Catfood.Shapefile;
using Microsoft.Maps.MapControl.WPF;
using ShapeFileLoading.Domain.Repositories;

namespace ShapeFileLoading.Domain.Services
{
    public class ShapeFileConvertersServices : IShapeFileConverters
    {
        public LocationCollection PointDArrayToLocationCollection(PointD[] points)
        {
            LocationCollection locations = new LocationCollection();

            int numPoints = points.Length;

            for (int i = 0; i < numPoints; i++)
                locations.Add(new Location(points[i].Y, points[i].X));

            return locations;
        }

        public LocationRect RectangleDToLocationRect(RectangleD bBox)
        {
            return new LocationRect(bBox.Top, bBox.Left, bBox.Bottom, bBox.Right);
        }
    }
}
