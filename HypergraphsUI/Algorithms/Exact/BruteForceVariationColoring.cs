using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Exact;

public class BruteForceVariationColoring : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.BruteForceVariations;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        ColorVariationColoring coloring = new ColorVariationColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}