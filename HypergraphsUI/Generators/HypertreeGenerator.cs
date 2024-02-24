using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class HypertreeGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Hypertree;

    public override Hypergraph Generate(int n, int m, int r = 1)
    {
        Hypergraphs.Generators.HypertreeGenerator generator = new Hypergraphs.Generators.HypertreeGenerator();
        return generator.Generate(n, m);
    }
    
}