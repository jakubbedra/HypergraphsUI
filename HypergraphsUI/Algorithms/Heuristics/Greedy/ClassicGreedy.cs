using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class ClassicGreedy : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.ClassicGreedy;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.ClassicGreedy coloring = new Hypergraphs.Algorithms.ClassicGreedy();
        return coloring.ComputeColoring(hypergraph);
    }
}