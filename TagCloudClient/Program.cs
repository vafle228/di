using System.Drawing;
using Autofac;
using CommandLine;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.CloudLayouter.PointLayouter;
using TagCloud.CloudLayouter.PointLayouter.PointGenerator;
using TagCloud.CloudLayouter.PointLayouter.PointGenerator.Generators;
using TagCloud.CloudLayouter.Settings.Generators;
using TagCloud.ImageGenerator;
using TagCloud.WordsFilter;
using TagCloud.WordsFilter.Filters;
using TagCloud.WordsReader;
using TagCloud.WordsReader.Readers;

namespace TagCloudClient;

internal class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(settings =>
            {
                var container = BuildContainer(settings);
                var generator = container.Resolve<CloudGenerator>();
                generator.GenerateTagCloud();
            });
    }

    private static IContainer BuildContainer(Options settings)
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(SettingsFactory.BuildBitmapSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildFileReaderSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildPolarSpiralSettings(settings)).AsSelf();
        builder.RegisterInstance(SettingsFactory.BuildSquareSpiralSettings(settings)).AsSelf();
        builder.Register(context => SettingsFactory.BuildPointLayouterSettings(
            settings, context.Resolve<IPointGenerator>())).AsSelf();

        builder
            .RegisterType<FileReader>().As<IWordsReader>()
            .OnlyIf(_ => Path.GetExtension(settings.Path) == ".txt");
        
        builder
            .RegisterType<PolarArchimedesSpiral>().As<IPointGenerator>()
            .OnlyIf(_ => settings.UsingGenerator == PossibleGenerators.POLAR_SPIRAL);
        
        builder
            .RegisterType<SquareArchimedesSpiral>().As<IPointGenerator>()
            .OnlyIf(_ => settings.UsingGenerator == PossibleGenerators.SQUARE_SPIRAL);
    
        builder.RegisterType<LowercaseFilter>().As<IWordsFilter>();
        builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();

        builder.RegisterType<BitmapGenerator>().AsSelf();
        builder.RegisterType<PointCloudLayouter>().As<ICloudLayouter>();
        
        builder.RegisterType<CloudGenerator>().AsSelf();
    
        return builder.Build();
    }
}