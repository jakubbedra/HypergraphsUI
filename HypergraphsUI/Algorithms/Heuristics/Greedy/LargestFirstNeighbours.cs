using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class LargestFirstNeighbours : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.LargestFirstNeighbours;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.LargestFirstNeighbours coloring = new Hypergraphs.Algorithms.LargestFirstNeighbours();
        return coloring.ComputeColoring(hypergraph);
    }
}