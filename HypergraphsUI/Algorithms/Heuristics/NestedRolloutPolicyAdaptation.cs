using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class NestedRolloutPolicyAdaptation : BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.NestedRolloutPolicyAdaptation;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        AlgorithmConstants.AllGeneratorTypes;

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        throw new System.NotImplementedException();
    }
}