using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class HyperstarGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Hyperstar;

    public override Hypergraph Generate(int n, int m, int c = 1)
    {
        Hypergraphs.Generators.HyperstarGenerator generator = new Hypergraphs.Generators.HyperstarGenerator();
        return generator.Generate(n, m, c);
    }
    
}