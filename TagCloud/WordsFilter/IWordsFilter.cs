namespace TagCloud.WordsFilter;

public interface IWordsFilter
{
    /*
     * Apply some function on words list
     */
    List<string> FilterWords(List<string> words);
}