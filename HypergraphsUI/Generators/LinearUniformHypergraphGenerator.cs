using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class LinearUniformHypergraphGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.LinearUniform;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.LinearUniformHypergraphGenerator generator = new Hypergraphs.Generators.LinearUniformHypergraphGenerator();
        return generator.Generate(n, m, r);
    }
    
}