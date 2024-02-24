using Hypergraphs.Generators;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public class RandomConnectedHypergraphGenerator : BaseGenerator
{
    public override GeneratorType GetType() => GeneratorType.Random;

    public override Hypergraph Generate(int n, int m, int r=1)
    {
        ConnectedHypergraphGenerator generator = new ConnectedHypergraphGenerator();
        return generator.Generate(n, m);
    }
}