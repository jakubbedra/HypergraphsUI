using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class BruteForcePermutationColoring : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.BruteForcePermutations;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        VertexPermutationColoring coloring = new VertexPermutationColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}