using System.Drawing;
using TagCloud.CloudLayouter.PointLayouter.PointGenerator;

namespace TagCloud.CloudLayouter.PointLayouter;

public class SpiralCloudLayouter(Point center, IPointGenerator pointGenerator) : ICloudLayouter
{
    private readonly List<Point> placedPoints = [];
    private readonly List<Rectangle> placedRectangles = [];

    public Point Center { get; } = center;

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle placedRect;
        try
        {
            placedRect = pointGenerator.StartFrom(Center)
                .Except(placedPoints)
                .Select(p => CreateRectangle(p, rectangleSize))
                .First(r => !placedRectangles.Any(r.IntersectsWith));
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("There are no more points in generator");
        }

        placedRectangles.Add(placedRect);
        placedPoints.Add(placedRect.Location - placedRect.Size / 2);
        
        return placedRect;
    }

    private static Rectangle CreateRectangle(Point center, Size rectangleSize)
    {
        var rectangleUpperLeft = center - rectangleSize / 2;
        return new Rectangle(rectangleUpperLeft, rectangleSize);
    }
}