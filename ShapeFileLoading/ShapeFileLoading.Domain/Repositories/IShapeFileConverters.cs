using Catfood.Shapefile;
using Microsoft.Maps.MapControl.WPF;

namespace ShapeFileLoading.Domain.Repositories
{
    public interface IShapeFileConverters
    {
        LocationRect RectangleDToLocationRect(RectangleD bBox);

        LocationCollection PointDArrayToLocationCollection(PointD[] points);
    }
}
