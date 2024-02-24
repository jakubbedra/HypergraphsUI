using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HyperpathColoring : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HyperpathColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hyperpath
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        throw new System.NotImplementedException();
    }
}