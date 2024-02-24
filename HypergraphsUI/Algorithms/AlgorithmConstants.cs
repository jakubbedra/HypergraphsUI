using System.Collections.Generic;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms;

public class AlgorithmConstants
{
    public static readonly HashSet<GeneratorType> AllGeneratorTypes =
        new HashSet<GeneratorType>()
        {
            GeneratorType.Random, GeneratorType.Hypercycle, GeneratorType.Hyperpath, GeneratorType.Hyperstar, GeneratorType.Hypertree, GeneratorType.Uniform, GeneratorType.LinearUniform, GeneratorType.ThreeUniform
        };

    public static readonly HashSet<Algorithm> AllAlgorithms =
        new HashSet<Algorithm>()
        {
            Algorithm.BaseGreedy, Algorithm.DSatur, Algorithm.HypercycleColoring, Algorithm.HyperpathColoring, Algorithm.HyperstarColoring, Algorithm.HypertreeColoring, Algorithm.Lovasz3Uniform, Algorithm.LovaszUniform, Algorithm.LovaszLinearUniform, Algorithm.BruteForcePermutations, Algorithm.BruteForceVariations, Algorithm.VertexDegreeGreedy, Algorithm.NestedMonteCarloSearch, Algorithm.NestedRolloutPolicyAdaptation
        };

}