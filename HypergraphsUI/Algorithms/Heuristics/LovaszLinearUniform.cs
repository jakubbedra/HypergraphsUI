using System.Collections.Generic;
using Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class LovaszLinearUniform: BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.LovaszLinearUniform;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.LinearUniform
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        LinearUniformHypergraphColoring coloring = new LinearUniformHypergraphColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}