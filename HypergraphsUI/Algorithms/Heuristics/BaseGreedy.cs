using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class BaseGreedy : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.BaseGreedy;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        GreedyColoring coloring = new GreedyColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}