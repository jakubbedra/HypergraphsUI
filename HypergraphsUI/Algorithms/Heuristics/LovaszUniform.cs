using System.Collections.Generic;
using Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms.Heuristics;

public class LovaszUniform: BaseAlgorithm
{
    public override Algorithm GetAlgorithm() => Algorithm.LovaszUniform;

    public override HashSet<GeneratorType> GetAllowedGeneratorTypes() =>
        new HashSet<GeneratorType>()
        {
            GeneratorType.Uniform, GeneratorType.LinearUniform
        };

    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        UniformHypergraphColoring coloring = new UniformHypergraphColoring();
        return coloring.ComputeColoring(hypergraph);
    }
}