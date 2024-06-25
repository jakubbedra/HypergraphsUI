using System.Collections.Generic;
using System.IO;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class HyperstarGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10;1", 0},
        {"25;25;5", 0},
        {"50;100;10", 0},
        {"100;50;10", 0},
    };
   
    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10;1", ""},
        {"25;25;5", ""},
        {"50;100;10", "" },
        {"100;50;10", "" },
    };
    
    public override GeneratorType GetType() => GeneratorType.Hyperstar;
    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\hyperstars\";

    public override Hypergraph Generate(int n, int m, int c = 1)
    {
        if (files[$"{n};{m};{c}"] == "")
            files[$"{n};{m};{c}"] = File.ReadAllText(path + $"hyperstars_{n}_{m}_{c}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m};{c}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m};{c}"]];
        sizesCounter[$"{n};{m};{c}"]++;
        return hypergraph;
    }
    
}