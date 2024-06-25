using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HyperpathGreedyColoringAdapter : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HyperpathGreedyColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hyperpath
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HyperpathGreedyColoring coloring = new HyperpathGreedyColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}