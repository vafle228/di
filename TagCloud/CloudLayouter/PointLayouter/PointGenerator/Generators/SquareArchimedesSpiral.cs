using System.Drawing;

namespace TagCloud.CloudLayouter.PointLayouter.PointGenerator.Generators;

public class SquareArchimedesSpiral(uint step) : IPointGenerator
{
    public uint Step { get; } = step;

    public IEnumerable<Point> StartFrom(Point startPoint)
    {
        var neededPoints = 1;
        var pointsToPlace = 1;
        var direction = Direction.Up;
        var currentPoint = Point.Empty;

        while (true)
        {
            yield return currentPoint;
            
            pointsToPlace--;
            currentPoint += GetOffsetSize(direction);
            
            if (pointsToPlace == 0)
            {
                direction = direction.AntiClockwiseRotate();
                if (direction is Direction.Up or Direction.Down) neededPoints++;
                pointsToPlace = neededPoints;
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    private Size GetOffsetSize(Direction direction) => direction switch
    {
        Direction.Up => new Size(0, (int)Step),
        Direction.Right => new Size((int)Step, 0),
        Direction.Down => new Size(0, -(int)Step),
        Direction.Left => new Size(-(int)Step, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };
}