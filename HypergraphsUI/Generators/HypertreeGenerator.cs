using System.Collections.Generic;
using System.IO;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class HypertreeGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10", 0},
        {"10;20", 0},
        {"20;10", 0},
        {"100;100", 0},
        {"100;500", 0},
        {"500;100", 0},
    };
    
    public override GeneratorType GetType() => GeneratorType.Hypertree;
    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\hypertrees\";

    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10", "" },
        {"10;20", "" },
        {"20;10", "" },
        {"100;100", "" },
        {"100;500", "" },
        {"500;100", "" },
    };
    
    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        if (files[$"{n};{m}"] == "")
            files[$"{n};{m}"] = File.ReadAllText(path + $"hypertrees_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m}"]];
        sizesCounter[$"{n};{m}"]++;
        return hypergraph;
    }
    
}