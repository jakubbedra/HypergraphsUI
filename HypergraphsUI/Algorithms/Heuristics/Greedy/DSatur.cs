using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class DSatur : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.DSatur;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        Hypergraphs.Algorithms.DSatur coloring = new Hypergraphs.Algorithms.DSatur();
        return coloring.ComputeColoring(hypergraph);
    }
}