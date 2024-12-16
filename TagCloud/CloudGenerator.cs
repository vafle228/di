using TagCloud.WordsFilter;
using TagCloud.WordsReader;

namespace TagCloud;

public class CloudGenerator(
    IWordsReader reader, 
    IEnumerable<IWordsFilter> filters)
{
    public string GenerateTagCloud()
    {
        var words = reader.ReadWords();

        var freqDict = filters
            .Aggregate(words, (c, f) => f.ApplyFilter(c))
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .ToDictionary(g => g.Key, g => g.Count());

        return string.Empty;
    }
}