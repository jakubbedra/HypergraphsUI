using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class ThreeColorableHypercycleGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"21;31", 0},
        {"49;49", 0},
        {"109;209", 0},
        {"21;49", 0},
        {"109;249", 0},//wrrrrrrrrrr
    };

    private static string path = @"C:\Users\theKonfyrm\Desktop\hypercycle3col\";

    public override GeneratorType GetType() => GeneratorType.Hypercycle;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        string jsonFromFile = File.ReadAllText(path + $"3_colorable_hypercycles_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(jsonFromFile);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m}"]];
        sizesCounter[$"{n};{m}"]++;
        return hypergraph;
    }
    
}
