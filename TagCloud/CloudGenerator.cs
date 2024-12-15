﻿using TagCloud.ImageGenerator;
using TagCloud.WordsFilter;
using TagCloud.WordsReader;

namespace TagCloud;

public class CloudGenerator(
    IWordsReader reader, 
    List<IWordsFilter> filters, 
    BitmapGenerator imageGenerator,
    CloudGeneratorSettings settings)
{
    public string GenerateTagCloud()
    {
        return string.Empty;
    }
}