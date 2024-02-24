using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class HyperpathGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Hyperpath;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.HyperpathGenerator generator = new Hypergraphs.Generators.HyperpathGenerator();
        return generator.Generate(n, m);
    }
}