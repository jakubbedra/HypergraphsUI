using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HypertreeColoringAdapter : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HypertreeColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hypertree
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HypertreeColoring coloring = new HypertreeColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}