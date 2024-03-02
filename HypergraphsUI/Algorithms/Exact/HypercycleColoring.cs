using System;
using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HypercycleColoring : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HypercycleColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hypercycle
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HypercycleColoring coloring = new HypercycleColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}