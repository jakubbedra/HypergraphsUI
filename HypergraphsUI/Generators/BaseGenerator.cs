using Hypergraphs.Model;
using HypergraphsUI.Algorithms;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Generators;

public abstract class BaseGenerator
{
    public abstract GeneratorType GetType();
    public abstract Hypergraph Generate(int n, int m, int r=1);

    public bool CompatibleWith(BaseAlgorithm algorithm)
    {
        return algorithm.GetAllowedGeneratorTypes().Contains(GetType());
    }
}