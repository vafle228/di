using System.Drawing;
using TagCloud.CloudLayouter;

namespace TagCloud.ImageGenerator;

#pragma warning disable CA1416
public class BitmapGenerator(BitmapSettings settings, ICloudLayouter layouter)
{
    public Bitmap GenerateWindowsBitmap(List<WordTag> tags)
    {
        var size = settings.Sizes;
        var bitmap = new Bitmap(size.Width, size.Height);
        using var graphics = Graphics.FromImage(bitmap);
        
        graphics.Clear(settings.BackgroundColor);
        var brush = new SolidBrush(settings.ForegroundColor);

        foreach (var tag in tags)
        {
            var font = new Font(settings.Font, tag.FontSize);
            var wordSize = graphics.MeasureString(tag.Word, font);
            
            var positionRect = layouter.PutNextRectangle(wordSize.ToSize());
            graphics.DrawString(tag.Word, font, brush, positionRect);
        }

        return bitmap;
    }
}
#pragma warning restore CA1416