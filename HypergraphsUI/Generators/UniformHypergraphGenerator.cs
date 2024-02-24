using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class UniformHypergraphGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Uniform;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.UniformHypergraphGenerator generator = new Hypergraphs.Generators.UniformHypergraphGenerator();
        return generator.GenerateConnected(n, m, r);
    }
    
}