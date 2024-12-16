using Autofac;
using CommandLine;
using TagCloud;
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
                Console.WriteLine(settings.UsingEncoding);
                var container = BuildContainer(settings);
                var generator = container.Resolve<CloudGenerator>();
                generator.GenerateTagCloud();
            });
    }

    private static IContainer BuildContainer(Options settings)
    {
        var builder = new ContainerBuilder();
    
        builder
            .Register(_ => new FileReader(settings.Path, settings.UsingEncoding))
            .As<IWordsReader>()
            .OnlyIf(_ => Path.GetExtension(settings.Path) == ".txt");
    
        builder.RegisterType<LowercaseFilter>().As<IWordsFilter>();
        builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();
        
        builder.RegisterType<CloudGenerator>().AsSelf();
    
        return builder.Build();
    }
}