using System.Collections.Generic;
using System.IO;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;
using Newtonsoft.Json;

namespace HypergraphsUI.Generators;

public class ThreeUniformHypergraphGenerator : BaseGenerator
{
    private static Dictionary<string, int> sizesCounter = new Dictionary<string, int>()
    {
        {"10;10;3", 0},
        {"10;20;3", 0},
        {"15;100;3", 0},
        {"15;200;3", 0},
        {"20;100;3", 0},
    };

    private static string path = @"C:\Users\theKonfyrm\Desktop\generators\3uniform\";
    public override GeneratorType GetType() => GeneratorType.ThreeUniform;

    private static Dictionary<string, string> files = new Dictionary<string, string>()
    {
        {"10;10;3", ""},
        {"10;20;3", ""},
        {"15;100;3", ""},
        {"15;200;3", ""},
        {"20;100;3", ""},
    };

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        if (files[$"{n};{m};{r}"] == "")
            files[$"{n};{m};{r}"] = File.ReadAllText(path + $"3uniform_{n}_{m}.json");
        List<Hypergraph> deserializedHypergraphs = JsonConvert.DeserializeObject<List<Hypergraph>>(files[$"{n};{m};{r}"]);
        Hypergraph hypergraph = deserializedHypergraphs[sizesCounter[$"{n};{m};{r}"]];
        sizesCounter[$"{n};{m};{r}"]++;
        return hypergraph;
    }
    
}