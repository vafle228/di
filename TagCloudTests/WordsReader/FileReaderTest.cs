using System.Text;
using FluentAssertions;
using TagCloud.WordsReader.Readers;
using TagCloudTests.WordsReader.Tools;

namespace TagCloudTests.WordsReader;

[TestFixture]
public class FileReaderTest
{
    private const string FILE_PATH = "Samples/text.txt";
    
    [Test]
    public void FileReader_ReadWords_ShouldReadAllWords()
    {
        var reader = new FileReader(FILE_PATH, Encoding.UTF8);
        var fileContent = File.ReadAllLines(FILE_PATH, Encoding.UTF8).ToText(" ");

        var words = reader.ReadWords();
        
        words.ToText(" ").Should().Be(fileContent);
    }
}