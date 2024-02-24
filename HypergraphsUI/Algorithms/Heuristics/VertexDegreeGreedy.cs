using System.Collections.Generic;
using Hypergraphs.Algorithms;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class VertexDegreeGreedy : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.VertexDegreeGreedy;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        SimplifiedDSatur coloring = new SimplifiedDSatur();
        return coloring.ComputeColoring(hypergraph);
    }
}