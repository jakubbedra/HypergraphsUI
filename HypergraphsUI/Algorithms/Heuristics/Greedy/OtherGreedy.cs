using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class OtherGreedy : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.OtherGreedy;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.OtherGreedy coloring = new Hypergraphs.Algorithms.OtherGreedy();
        return coloring.ComputeColoring(hypergraph);
    }
}