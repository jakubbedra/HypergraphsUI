using System.Collections.Generic;
using System.IO;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class HyperpathGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10", 0},
        {"10;20", 0},
        {"20;10", 0},
        {"25;25", 0},
        {"50;20", 0},
        {"20;50", 0},
        {"100;100", 0},
        {"100;200", 0},
        {"200;100", 0},
    };
    
    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10", "" },
        {"10;20", "" },
        {"20;10", "" },
        {"25;25", "" },
        {"50;20", "" },
        {"20;50", "" },
        {"100;100", "" },
        {"100;200", "" },
        {"200;100", "" },
    };

    public override GeneratorType GetType() => GeneratorType.Hyperpath;
    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\hyperpaths\";

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        if (files[$"{n};{m}"] == "")
            files[$"{n};{m}"] = File.ReadAllText(path + $"hyperpaths_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m}"]];
        sizesCounter[$"{n};{m}"]++;
        return hypergraph;
    }
}