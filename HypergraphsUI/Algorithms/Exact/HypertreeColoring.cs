using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HypertreeColoring : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HypertreeColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hypertree
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.HypertreeColoring coloring = new Hypergraphs.Algorithms.HypertreeColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}