using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class HypercycleGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Hypercycle;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.HypercycleGenerator generator = new Hypergraphs.Generators.HypercycleGenerator();
        return generator.Generate(n, m);
    }
    
}