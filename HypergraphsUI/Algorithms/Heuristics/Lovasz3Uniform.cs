using System.Collections.Generic;
using Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class Lovasz3Uniform: BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.Lovasz3Uniform;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.ThreeUniform
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        ThreeUniformHypergraphColoring coloring = new ThreeUniformHypergraphColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}