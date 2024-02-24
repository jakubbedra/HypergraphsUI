using System;
using System.Collections.Generic;
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
        throw new NotImplementedException();
        // Hypergraphs.Algorithms.NestedMonteCarloSearch coloring = new Hypergraphs.Algorithms.NestedMonteCarloSearch();
        // return coloring.ComputeColoring(hypergraph);
    }
}