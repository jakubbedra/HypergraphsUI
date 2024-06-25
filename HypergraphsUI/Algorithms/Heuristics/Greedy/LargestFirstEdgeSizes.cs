using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class LargestFirstEdgeSizes : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.LargestFirstEdgeSizes;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.LargestFirstEdgeSizes coloring = new Hypergraphs.Algorithms.LargestFirstEdgeSizes();
        return coloring.ComputeColoring(hypergraph);
    }
}