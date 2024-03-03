using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HyperpathColoringAdapter : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HyperpathColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hyperpath
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HyperpathColoring coloring = new HyperpathColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}