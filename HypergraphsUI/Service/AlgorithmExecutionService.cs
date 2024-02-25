using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hypergraphs.Model;
using HypergraphsUI.Algorithms;
using HypergraphsUI.Algorithms.Exact;
using HypergraphsUI.Algorithms.Heuristics;
using HypergraphsUI.Generators;
using HypergraphsUI.Model;
using HypergraphsUI.ViewModel;

public class AlgorithmExecutionService
{
    private Dictionary<GeneratorType, BaseGenerator> _generators;
    private Dictionary<Algorithm, BaseAlgorithm> _algorithms;

    public AlgorithmExecutionService()
    {
        _generators = new Dictionary<GeneratorType, BaseGenerator>();
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
        _algorithms.Add(Algorithm.HypertreeColoring, new HypertreeColoring());
        _algorithms.Add(Algorithm.HyperpathColoring, new HyperpathColoring());
        _algorithms.Add(Algorithm.HypercycleColoring, new HypercycleColoring());
        _algorithms.Add(Algorithm.BruteForcePermutations, new BruteForcePermutationColoring());
        _algorithms.Add(Algorithm.BruteForceVariations, new BruteForceVariationColoring());
        _algorithms.Add(Algorithm.Lovasz3Uniform, new Lovasz3Uniform());
        _algorithms.Add(Algorithm.LovaszUniform, new LovaszUniform());
        _algorithms.Add(Algorithm.LovaszLinearUniform, new LovaszLinearUniform());
        _algorithms.Add(Algorithm.NestedMonteCarloSearch, new NestedMonteCarloSearch());
        _algorithms.Add(Algorithm.NestedRolloutPolicyAdaptation, new NestedRolloutPolicyAdaptation());
    }

    public ExecutionResult Execute(ExecutionRequest request)
    {
        List<ColoringResult> results = new List<ColoringResult>();
        
        foreach (HypergraphRequest hypergraphRequest in request.Hypergraphs)
        {
            foreach (string size in hypergraphRequest.Sizes)
            {
                Dictionary<Algorithm, long> avgExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> minExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> maxExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, int> avgUsedColors = new Dictionary<Algorithm, int>();
                Dictionary<Algorithm, int> minUsedColors = new Dictionary<Algorithm, int>();
                Dictionary<Algorithm, int> maxUsedColors = new Dictionary<Algorithm, int>();
                for (int i = 0; i < request.HypergraphsCount; i++)
                {
                    Hypergraph hypergraph = GenerateHypergraph(size.Split(";").Select(s => int.Parse(s)).ToArray(), hypergraphRequest.GeneratorType);
                    foreach (Algorithm algorithm in request.ChosenAlgorithms)
                    {
                        avgExecutionTimes.TryAdd(algorithm, 0);
                        minExecutionTimes.TryAdd(algorithm, 0);
                        maxExecutionTimes.TryAdd(algorithm, 0);
                        SimpleColoringResult coloringResult = ExecuteHypergraphColoring(hypergraph, algorithm, request.IterationCount);
                        avgExecutionTimes[algorithm] += coloringResult.Time;
                        minExecutionTimes[algorithm] = Math.Min(minExecutionTimes[algorithm], coloringResult.Time);
                        maxExecutionTimes[algorithm] = Math.Max(maxExecutionTimes[algorithm], coloringResult.Time);
                        avgUsedColors[algorithm] += coloringResult.UsedColors;
                        minUsedColors[algorithm] = Math.Min(minUsedColors[algorithm], coloringResult.UsedColors);
                        maxUsedColors[algorithm] = Math.Max(maxUsedColors[algorithm], coloringResult.UsedColors);
                    }
                }
                foreach (Algorithm algorithm in request.ChosenAlgorithms)
                {
                    ColoringResult result = new ColoringResult()
                    {
                        Algorithm = algorithm,
                        HypergraphType = hypergraphRequest.GeneratorType,
                        AvgExecutionTime = avgExecutionTimes[algorithm] / request.HypergraphsCount,
                        MinExecutionTime = minExecutionTimes[algorithm],
                        MaxExecutionTime = maxExecutionTimes[algorithm],
                        DistinctHypergraphs = request.HypergraphsCount,
                        Iterations = request.IterationCount,
                        Size = size,
                        AvgUsedColors = avgUsedColors[algorithm] / request.HypergraphsCount,
                        MinUsedColors = minUsedColors[algorithm],
                        MaxUsedColors = maxUsedColors[algorithm]
                    };
                    results.Add(result);
                }
            }

            return new ExecutionResult()
            {
                SuccessfulColorings = 0,
                Results = results
            };
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
    
    private Hypergraph GenerateHypergraph(int[] size, GeneratorType generatorType)
    {
        BaseGenerator generator = _generators[generatorType];
        if (size.Length < 2) throw new ArgumentException("Invalid size.");
        if (size.Length == 3)
        {
            return generator.Generate(size[0], size[1], size[2]);
        }

        return generator.Generate(size[0], size[1]);
    }

    private SimpleColoringResult ExecuteHypergraphColoring(Hypergraph hypergraph, Algorithm algorithm, int iterations)
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
        return new SimpleColoringResult()
        {
            UsedColors = colors.Distinct().Count(),
            Time = ((executionMillis - loopMillis) / iterations)
        };
    }
    
}