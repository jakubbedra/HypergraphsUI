using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class HypercycleGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10", 0},
        {"10;20", 0},
        {"50;20", 0},
        {"50;50", 0},
        {"50;100", 0},
        {"100;100", 0},
        {"100;200", 0},
        {"200;100", 0},
    };

    private static string path = @"C:\Users\theKonfyrm\Desktop\hypercycle\";
    
    public override GeneratorType GetType() => GeneratorType.Hypercycle;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        string jsonFromFile = File.ReadAllText(path + $"hypercycles_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(jsonFromFile);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m}"]];
        sizesCounter[$"{n};{m}"]++;
        return hypergraph;
    }
    
}
