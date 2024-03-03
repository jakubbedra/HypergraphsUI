using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HyperstarColoringAdapter : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HyperstarColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hyperstar
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HyperstarColoring coloring = new HyperstarColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}