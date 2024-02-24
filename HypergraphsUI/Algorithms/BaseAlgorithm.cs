using System.Collections.Generic;
using Hypergraphs.Model;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms;

public abstract class BaseAlgorithm
{
    public abstract Algorithm GetAlgorithm();
    public abstract HashSet<GeneratorType> GetAllowedGeneratorTypes();

    public abstract int[] ComputeColoring(Hypergraph hypergraph);
}