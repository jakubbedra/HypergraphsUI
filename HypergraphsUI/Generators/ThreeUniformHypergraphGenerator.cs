using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class ThreeUniformHypergraphGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.ThreeUniform;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.UniformHypergraphGenerator generator = new Hypergraphs.Generators.UniformHypergraphGenerator();
        return generator.GenerateConnected(n, m, 3);
    }
    
}