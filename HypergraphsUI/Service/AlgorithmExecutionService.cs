using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hypergraphs.Model;
using HypergraphsUI.Algorithms;
using HypergraphsUI.Algorithms.Heuristics;
using HypergraphsUI.Generators;
using HypergraphsUI.Model;
using HypergraphsUI.ViewModel;
using HypercycleGenerator = HypergraphsUI.Generators.HypercycleGenerator;
using HyperpathGenerator = HypergraphsUI.Generators.HyperpathGenerator;
using HyperstarColoring = HypergraphsUI.Algorithms.Exact.HyperstarColoring;
using HyperstarGenerator = HypergraphsUI.Generators.HyperstarGenerator;
using HypertreeGenerator = HypergraphsUI.Generators.HypertreeGenerator;
using LinearUniformHypergraphGenerator = HypergraphsUI.Generators.LinearUniformHypergraphGenerator;
using UniformHypergraphGenerator = HypergraphsUI.Generators.UniformHypergraphGenerator;

public class AlgorithmExecutionService
{
    private Dictionary<GeneratorType, BaseGenerator> _generators;
    private Dictionary<Algorithm, BaseAlgorithm> _algorithms;

    public AlgorithmExecutionService()
    {
        _generators = new Dictionary<GeneratorType, HypergraphsUI.Generators.BaseGenerator>();
        _generators.Add(GeneratorType.Random, new RandomConnectedHypergraphGenerator());
        _generators.Add(GeneratorType.Hyperstar, new HyperstarGenerator());
        _generators.Add(GeneratorType.Hypertree, new HypertreeGenerator());
        _generators.Add(GeneratorType.Hyperpath, new HyperpathGenerator());
        _generators.Add(GeneratorType.Hypercycle, new HypercycleGenerator());
        _generators.Add(GeneratorType.Uniform, new UniformHypergraphGenerator());
        _generators.Add(GeneratorType.ThreeUniform, new ThreeUniformHypergraphGenerator());
        _generators.Add(GeneratorType.LinearUniform, new LinearUniformHypergraphGenerator());

        _algorithms = new Dictionary<Algorithm, BaseAlgorithm>();
        _algorithms.Add(Algorithm.BaseGreedy, new BaseGreedy());
        _algorithms.Add(Algorithm.DSatur, new DSatur());
        _algorithms.Add(Algorithm.VertexDegreeGreedy, new VertexDegreeGreedy());
        _algorithms.Add(Algorithm.HyperstarColoring, new HypergraphsUI.Algorithms.Exact.HyperstarColoring());
        _algorithms.Add(Algorithm.HypertreeColoring, new HypergraphsUI.Algorithms.Exact.HypertreeColoring());
        _algorithms.Add(Algorithm.HyperpathColoring, new HypergraphsUI.Algorithms.Exact.HyperpathColoring());
        _algorithms.Add(Algorithm.HypercycleColoring, new HypergraphsUI.Algorithms.Exact.HypercycleColoring());
        _algorithms.Add(Algorithm.BruteForcePermutations, new HypergraphsUI.Algorithms.Exact.BruteForcePermutationColoring());
        _algorithms.Add(Algorithm.BruteForceVariations, new HypergraphsUI.Algorithms.Exact.BruteForceVariationColoring());
        _algorithms.Add(Algorithm.Lovasz3Uniform, new Lovasz3Uniform());
        _algorithms.Add(Algorithm.LovaszUniform, new LovaszUniform());
        _algorithms.Add(Algorithm.LovaszLinearUniform, new LovaszLinearUniform());
        _algorithms.Add(Algorithm.NestedMonteCarloSearch, new NestedMonteCarloSearch());
        _algorithms.Add(Algorithm.NestedRolloutPolicyAdaptation, new NestedRolloutPolicyAdaptation());
    }

    public ExecutionResult Execute(ExecutionRequest request)
    {
        List<ColoringResult> results = new List<ColoringResult>();
        foreach (GeneratorType generatorType in request.GeneratorTypes)
        {
            foreach (HypergraphSize size in request.HypergraphSizes)
            {
                Hypergraph hypergraph = GenerateHypergraph(size, generatorType);
                foreach (Algorithm algorithm in request.ChosenAlgorithms)
                {
                    ColoringResult coloringResult = ExecuteHypergraphColoring(hypergraph, algorithm,
                        request.IterationCount, generatorType, size);
                    results.Add(coloringResult);
                }
            }
        }

        return new ExecutionResult()
        {
            SuccessfulColorings = results.Count,
            Results = results
        };
    }

    public bool IsAlgorithmCompatible(Algorithm algorithm, List<GeneratorType> generators)
    {
        return generators.All(generator => _algorithms[algorithm].GetAllowedGeneratorTypes().Contains(generator));
    }

    public bool IsGeneratorCompatible(GeneratorType generator, List<Algorithm> algorithms)
    {
        return algorithms.All(algorithm => _algorithms[algorithm].GetAllowedGeneratorTypes().Contains(generator));
    }
    
    private Hypergraph GenerateHypergraph(HypergraphSize size, GeneratorType generatorType)
    {
        BaseGenerator generator = _generators[generatorType];
        if (generatorType is GeneratorType.LinearUniform or GeneratorType.Uniform or GeneratorType.ThreeUniform)
        {
            UniformHypergraphSize uniformSize = (UniformHypergraphSize)size;
            return generator.Generate(uniformSize.N, uniformSize.M, uniformSize.R);
        }

        if (generatorType == GeneratorType.Hyperstar)
        {
            HyperstarSize hyperstarSize = (HyperstarSize)size;
            return generator.Generate(hyperstarSize.N, hyperstarSize.M, hyperstarSize.C);
        }

        return generator.Generate(size.N, size.M);
    }

    private ColoringResult ExecuteHypergraphColoring(
        Hypergraph hypergraph, Algorithm algorithm, int iterations, GeneratorType generatorType, HypergraphSize size
    )
    {
        BaseAlgorithm coloringAlgorithm = _algorithms[algorithm];
        int[] colors = new int [0];

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            colors = coloringAlgorithm.ComputeColoring(hypergraph);
        }
        stopwatch.Stop();
        var executionMillis = stopwatch.ElapsedMilliseconds;


        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
        }

        stopwatch.Stop();
        var loopMillis = stopwatch.ElapsedMilliseconds;
        return new ColoringResult()
        {
            HypergraphType = generatorType,
            Size = size,
            Delta = hypergraph.Delta(),
            Iterations = iterations,
            ChromaticNumber = colors.Distinct().Count(),
            ExecutionTime = ((executionMillis - loopMillis) / iterations)
        };
    }
    
}