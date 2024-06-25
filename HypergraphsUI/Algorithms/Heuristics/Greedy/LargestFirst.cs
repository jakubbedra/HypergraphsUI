using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class LargestFirst : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.LargestFirst;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.LargestFirst coloring = new Hypergraphs.Algorithms.LargestFirst();
        return coloring.ComputeColoring(hypergraph);
    }
}