using System.Collections.Generic;
using System.IO;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class UniformHypergraphGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10;4", 0},
        {"10;20;4", 0},
        {"15;100;4", 0},
        {"15;200;4", 0},
        {"20;200;4", 0},
    };

    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10;4", "" },
        {"10;20;4", "" },
        {"15;100;4", "" },
        {"15;200;4", "" },
        {"20;200;4", "" },
    };
    
    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\uniform\";
    public override GeneratorType GetType() => GeneratorType.Uniform;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        if (files[$"{n};{m};{r}"] == "")
            files[$"{n};{m};{r}"] = File.ReadAllText(path + $"uniform_{n}_{m}_{r}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m};{r}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m};{r}"]];
        sizesCounter[$"{n};{m};{r}"]++;
        return hypergraph;
    }
    
}