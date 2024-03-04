using System;
using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class NestedMonteCarloSearch : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.NestedMonteCarloSearch;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        NMCSColoring coloring = new NMCSColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}