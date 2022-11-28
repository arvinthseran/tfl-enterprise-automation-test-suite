﻿namespace Redbridge.UITests.Support;

public class TestDataHelper
{
    public TestDataHelper()
    {
        Postcode = GetRandomElementFromListOfElements(_valid_postcode);
    }

    public string Postcode { get; private set; }

    public T GetRandomElementFromListOfElements<T>(List<T> elements)
    {
        var randomNumber = new Random().Next(0, elements.Count);

        return elements[randomNumber];
    }

    private static readonly List<string> _valid_postcode = new() { "IG1 3DF", "IG1 3DE", "IG1 3DH", "Mayfair Avenue" };

    private static readonly List<string> _invalid_postcode = new() { "IG1 63DF", "IG12 3DE" };

    private static readonly List<string> _error_postcode = new() { "IG1X 3DH" };
}