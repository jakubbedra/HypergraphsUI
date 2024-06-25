using System.Collections.Generic;
using System.IO;
using Hypergraphs.Generators;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class RandomConnectedHypergraphGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10", 0},
        {"10;20", 0},
        {"20;10", 0},
        {"20;20", 0},
        {"20;100", 0},
    };
    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10", "" },
        {"10;20", "" },
        {"20;10", "" }, 
        {"20;20", "" },
        {"20;100", "" },
    };
    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\random\";
    public override GeneratorType GetType() => GeneratorType.Random;

    public override Hypergraph Generate(int n, int m, int r=1)
    {
        if (files[$"{n};{m}"] == "")
            files[$"{n};{m}"] = File.ReadAllText(path + $"random_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m}"]];
        sizesCounter[$"{n};{m}"]++;
        return hypergraph;
    }
}