using System.Text;
using FluentAssertions;
using TagCloud.WordsReader.Readers;
using TagCloud.WordsReader.Settings;
using TagCloudTests.WordsReader.Tools;

namespace TagCloudTests.WordsReader;

[TestFixture]
public class FileReaderTest
{
    private const string FILE_PATH = "Samples/text.txt";
    
    [Test]
    public void FileReader_ReadWords_ShouldReadAllWords()
    {
        var settings = new FileReaderSettings(FILE_PATH, Encoding.UTF8);
        var reader = new FileReader(settings);
        var fileContent = File.ReadAllLines(FILE_PATH, Encoding.UTF8).ToText(" ");

        var words = reader.ReadWords();
        
        words.ToText(" ").Should().Be(fileContent);
    }
}