using System;
using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class HypercycleColoringAdapter : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.HypercycleColoring;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Hypercycle
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        HypercycleColoring coloringAdapter = new HypercycleColoring();
        return coloringAdapter.ComputeColoring(hypergraph);
    }
}