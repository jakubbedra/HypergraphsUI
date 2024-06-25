using System.Collections.Generic;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Algorithms;

public class AlgorithmConstants
{
    public static readonly HashSet<GeneratorType> AllGeneratorTypes =
        new HashSet<GeneratorType>()
        {
            GeneratorType.Random, GeneratorType.Hypercycle, GeneratorType.ThreeColorableHypercycle, GeneratorType.Hyperpath, GeneratorType.Hyperstar, GeneratorType.Hypertree, GeneratorType.Uniform, GeneratorType.ThreeUniform
        };

    public static readonly HashSet<Algorithm> AllAlgorithms =
        new HashSet<Algorithm>()
        {
            Algorithm.ClassicGreedy, Algorithm.BaseGreedy, Algorithm.DSatur, Algorithm.HypercycleColoring, Algorithm.HyperpathColoring, Algorithm.HyperpathGreedyColoring, Algorithm.HyperstarColoring, Algorithm.HypertreeColoring, Algorithm.Lovasz3Uniform, Algorithm.LovaszUniform, Algorithm.CUDABruteForce, Algorithm.LargestFirst, Algorithm.NestedMonteCarloSearch, Algorithm.NestedRolloutPolicyAdaptation, Algorithm.OtherGreedy, Algorithm.LargestFirstNeighbours, Algorithm.LargestFirstEdgeSizes
        };

}