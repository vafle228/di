using TagCloud.ImageGenerator;
using TagCloud.WordsReader.Settings;

namespace TagCloudClient;

public static class SettingsFactory
{
    public static FileReaderSettings BuildFileReaderSettings(Options options)
        => new(options.Path, options.UsingEncoding);

    public static BitmapSettings BuildBitmapSettings(Options options)
        => new(options.Size, options.Font, options.ImageName, options.BackgroundColor, options.ForegroundColor);
}